using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;

namespace Console {
	public class TimetableCreator {
		private static string urlSeparator = "<;>";
		private static string resultFileName = "result.xlsx";
		private const string fileName = "Шаблон.xlsx";

		public static void Create() {
			var columnCount = 4;
			byte[] byteArray = File.ReadAllBytes(fileName);
			using (MemoryStream mem = new MemoryStream()) {
				mem.Write(byteArray, 0, (int)byteArray.Length);
				using (var document = SpreadsheetDocument.Open(mem, true)) {
					var rows = ReadData(document).Where(x => x.Length > 0).ToList();
					var relations = GetSheetData(document, "Relations").ToDictionary(x => x[1], x => x[0]);
					DeleteAWorkSheet(document, "Data");
					DeleteAWorkSheet(document, "Relations");
					WorksheetPart currentPart = null;

					var style = new List<UInt32Value>();
					var currentRow = 0;
					var count = rows.Count;
					for (var index = 0; index < count; index++) {
						System.Console.Clear();
						System.Console.WriteLine((index + 1) + "/" + count);
						var row = rows[index];
						if (row.Length == 1) {
							var name = relations.GetValueOrDefault(row[0]);
							if (name == null) {
								currentPart = null;
								continue;
							}
							currentPart = (WorksheetPart)document.WorkbookPart
								.GetPartById(GetPartId(document, name));
							currentRow = 0;
							continue;
						}
						if (currentPart == null)
							continue;
						for (int j = 0; j < columnCount; j++) {
							var column = (char)('A' + j);
							var cell = InsertCellInWorksheet(column.ToString(),
								(uint)(3 + currentRow), currentPart.Worksheet);
							var text = row[j];
							var withUrl = text.Contains(urlSeparator);
							var url = string.Empty;
							if (withUrl) {
								var parts = text.Split(new[] { urlSeparator }, StringSplitOptions.None);
								text = parts[0];
								url = parts[1];
							}
							cell.CellValue = new CellValue(text);
							cell.DataType = new EnumValue<CellValues>(CellValues.String);
								if (index == 0 || style.Count <= j + 1)
									style.Add(cell.StyleIndex);
								else
									cell.StyleIndex = style[j];
							if (withUrl)
								AddLink(currentPart, url, cell);
						}
						currentRow++;
					}

					document.WorkbookPart.Workbook.Save();
				}

				   using (FileStream fileStream = new FileStream(resultFileName,
                System.IO.FileMode.Create))
            {
                mem.WriteTo(fileStream);
            }
			}

		}

		private static List<string[]> ReadData(SpreadsheetDocument document) {
			List<string[]> result = GetSheetData(document, "Data");
			return result;
		}


		private static List<string[]> GetSheetData(SpreadsheetDocument document, string sheetName) {
			List<string[]> result;
			var partId = GetPartId(document, sheetName);
			var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(partId);
			var strings =
				document.WorkbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>();
			var relations = worksheetPart.HyperlinkRelationships.ToList();
			var links =
				worksheetPart.Worksheet.Elements<Hyperlinks>().GetOrDefault(x => x.FirstOrDefault())
					?? new Hyperlinks();

			var rows = worksheetPart.Worksheet.GetFirstChild<SheetData>()
				.Elements<Row>();

			result = rows.Select(x => x.Elements<Cell>()
				.Select(c =>
					new {
						Value = StringUtils.ParseInt(c.CellValue.GetOrDefault(k => k.Text)),
						Url = links.Cast<Hyperlink>().FirstOrDefault(h => h.Reference.Value == c.CellReference.Value)
							.GetOrDefault(h => relations.First(r => r.Id == h.Id).Uri.ToString())
					})
				.Where(z => z.Value.HasValue)
				.Select(
					(y, i) => strings.ElementAt(y.Value.Value).Text.Text + (y.Url == null || i > 0 ? "" : (urlSeparator + y.Url)))
				.ToArray()).ToList();
			return result;
		}

		public static void AddLink(WorksheetPart worksheetPart, string url, Cell cell) {
			var hyperlinks1 = new Hyperlinks();
			var rel = worksheetPart.AddHyperlinkRelationship(new Uri(url, UriKind.Absolute), true);

			var hyperlink1 = new Hyperlink() {
				Reference = cell.CellReference, Id = rel.Id
			};

			hyperlinks1.Append(hyperlink1);

			var pageMargins = worksheetPart.Worksheet.Descendants<PageMargins>().First();

			worksheetPart.Worksheet.InsertBefore(hyperlinks1, pageMargins);

			/*var hlRelationship =
				worksheetPart.AddHyperlinkRelationship(new Uri(url, UriKind.Absolute), true);

			var hyperlinks1 = new Hyperlinks();
			var hyperlink1 = new Hyperlink() {Reference = cell.CellReference, Id = hlRelationship.Id};
			hyperlinks1.Append(hyperlink1);

			var pageMargins1 = worksheetPart.Worksheet.Descendants<PageMargins>().First();
			pageMargins1.Remove();
			worksheetPart.Worksheet.Append(hyperlinks1);
			worksheetPart.Worksheet.Append(pageMargins1);*/
		}

		private static StringValue GetPartId(SpreadsheetDocument document, string name) {
			return document.WorkbookPart.Workbook.Sheets.Cast<Sheet>().First(x => x.Name == name).Id;
		}

		private static Cell GetSpreadsheetCell(Worksheet worksheet, string columnName, uint rowIndex) {
			IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex);
			if (rows.Count() == 0) {
				// A cell does not exist at the specified row.
				return null;
			}

			IEnumerable<Cell> cells =
				rows.First().Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0);
			if (cells.Count() == 0) {
				// A cell does not exist at the specified column, in the specified row.
				return null;
			}

			return cells.First();
		}


		private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, Worksheet worksheet) {
			SheetData sheetData = worksheet.GetFirstChild<SheetData>();
			string cellReference = columnName + rowIndex;

			// If the worksheet does not contain a row with the specified row index, insert one.
			Row row;
			if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0) {
				row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
			} else {
				row = new Row() {
					RowIndex = rowIndex
				};
				sheetData.Append(row);
			}

			// If there is not a cell with the specified column name, insert one.  
			if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0) {
				return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
			} else {
				// Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
				Cell refCell = null;
				foreach (Cell cell in row.Elements<Cell>()) {
					if (string.Compare(cell.CellReference.Value, cellReference, true) > 0) {
						refCell = cell;
						break;
					}
				}

				Cell newCell = new Cell() {
					CellReference = cellReference
				};
				row.InsertBefore(newCell, refCell);

				worksheet.Save();
				return newCell;
			}
		}

		private static void DeleteAWorkSheet(SpreadsheetDocument document, string sheetToDelete) {
			WorkbookPart wbPart = document.WorkbookPart;

			// Get the pivot Table Parts
			IEnumerable<PivotTableCacheDefinitionPart> pvtTableCacheParts = wbPart.PivotTableCacheDefinitionParts;
			Dictionary<PivotTableCacheDefinitionPart, string> pvtTableCacheDefinationPart =
				new Dictionary<PivotTableCacheDefinitionPart, string>();
			foreach (PivotTableCacheDefinitionPart Item in pvtTableCacheParts) {
				PivotCacheDefinition pvtCacheDef = Item.PivotCacheDefinition;
				//Check if this CacheSource is linked to SheetToDelete
				var pvtCahce = pvtCacheDef.Descendants<CacheSource>().Where(s => s.WorksheetSource.Sheet == sheetToDelete);
				if (pvtCahce.Count() > 0) {
					pvtTableCacheDefinationPart.Add(Item, Item.ToString());
				}
			}
			foreach (var Item in pvtTableCacheDefinationPart) {
				wbPart.DeletePart(Item.Key);
			}
			//Get the SheetToDelete from workbook.xml
			Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetToDelete).FirstOrDefault();
			if (theSheet == null) {
				// The specified sheet doesn't exist.
			}
			//Store the SheetID for the reference
			var Sheetid = theSheet.SheetId;

			// Remove the sheet reference from the workbook.
			WorksheetPart worksheetPart = (WorksheetPart)(wbPart.GetPartById(theSheet.Id));
			theSheet.Remove();

			// Delete the worksheet part.
			wbPart.DeletePart(worksheetPart);

			//Get the DefinedNames
			var definedNames = wbPart.Workbook.Descendants<DefinedNames>().FirstOrDefault();
			if (definedNames != null) {
				foreach (DefinedName Item in definedNames) {
					// This condition checks to delete only those names which are part of Sheet in question
					if (Item.Text.Contains(sheetToDelete + "!"))
						Item.Remove();
				}
			}
			// Get the CalculationChainPart 
			//Note: An instance of this part type contains an ordered set of references to all cells in all worksheets in the 
			//workbook whose value is calculated from any formula

			CalculationChainPart calChainPart;
			calChainPart = wbPart.CalculationChainPart;
			if (calChainPart != null) {
				var calChainEntries =
					calChainPart.CalculationChain.Descendants<CalculationCell>().Where(c => c.SheetId.ToString() == Sheetid);
				foreach (CalculationCell Item in calChainEntries) {
					Item.Remove();
				}
				if (calChainPart.CalculationChain.Count() == 0) {
					wbPart.DeletePart(calChainPart);
				}
			}
		}
	}
}