using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Specialist.Entities.Utils;
using Specialist.Web.Cms.Root.YandexDirect.Logic;

namespace Specialist.Web.Cms.Util {
	public class BinDatabase<T> {

		private readonly string _file;
		public BinDatabase(string file) {
			_file = file;
		}

		private List<T> _items;

		public List<T> Items {
		[MethodImpl(MethodImplOptions.Synchronized)]
			get {
			if(_items == null) {
				if(File.Exists(_file))
					_items = Serializer.Deserialize<List<T>>(_file);
				else
					_items = new List<T>();
			}
			return _items;
		}} 

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Add(T item) {
			Items.Insert(0,item);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Save() {
			Serializer.Serialize(_file, Items);
		}
		 
	}
}