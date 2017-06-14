using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.Extension;
using Specialist.Entities.Context.Extension;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context.Const;
using Specialist.Services.Order.Extension;

namespace Specialist.Entities.ViewModel
{
    public class CourseBaseVM
    {

	    public class SecondCourseDiscount {
		    public CourseLink SecondCourse { get; set; }

		    public decimal Discount { get; set; }

		    public decimal SumWithDiscount { get; set; }

		    public decimal Sum {
			    get { return Discount + SumWithDiscount; }
		    }
	    }

		public SecondCourseDiscount SecondCourse { get; set; }

		public bool HasTracks { get; set; }

		public decimal? UnlimitPrice { get; set; }
    	private List<PriceType> _distanceTypes;
    	public Course Course { get; set; }

		public List<CertType> CertTypeList { get; set; } 

	    public bool CourseInDiplom { get; set; }

		public int CompleteCourseCount { get; set; }
        public List<Certification> Certifications { get; set; }

        public List<Exam> Exams { get {
            return Course.ExamCourses.Select(ec => ec.Exam).ToList();
        }}

    	public bool HasCertExams {
    		get { return Exams.Any() || Certifications.Any(); }
    	}

        public List<PriceView> Prices { get; set; }

	    public decimal MinPriceForCredit {
		    get { return GetPrice(PriceTypes.Main) ?? 0; }
	    }

		public string HtmlTitle {
			get {
				if (Course.WebTopic != null) return Course.WebTopic;
				var name = Course.GetName();
				var withPrefix = WithCoursePrefix(name);
				return withPrefix + " в " +
						StringUtils.AngleBrackets("Специалист");
			}
		}

		public static string WithCoursePrefix(string name) {
			var prefix = "Курс ";
			if (name.StartsWith(prefix))
				prefix = String.Empty;
			var withPrefix = prefix + name;
			return withPrefix;
		}

    	public bool HasFullTimePrice {
    		get { return Prices.HasFullTimePrice(); }
    	}

    	public List<PriceType> DistanceTypes {
    		get {
    			return _distanceTypes ?? (_distanceTypes =
    				Prices.Select(p => p.PriceType)
    					.DistinctByPK()
    					.Where(pt => pt.PriceListType_TC == PriceListTypes.Distance)
    					.OrderBy(pt => pt.SortOrder).ToList());
    		}
    	}

    	public bool HasDistancePrice {
    		get { return DistanceTypes.Any(); }
    	}

    	public bool HasWebinar {
    		get { return GetPrice(PriceTypes.Webinar) > 0; }
    	}

    	public bool HasIntraExtra {
    		get { return Course.IsTrackBool && GetPrice(PriceTypes.IntraExtra) > 0; }
    	}

    	public bool HasIndividual {
    		get { return GetPrice(PriceTypes.Individual) > 0; }
    	}

	    public bool ShowDepartureInfo {
		    get { return !Course.IsSchool; }
	    }

	    public short? MorningDiscount { get; set; }

    	public List<string> Tabs {
    		get {
    			var result = new List<string>();
    			result.Add("Очное обучение");
    			return result;
    		}
    	}


    	public decimal? GetPriceContains(string priceTypeTC) {
            return Prices.FirstOrDefault(
                p => p.PriceType_TC.Contains(priceTypeTC))
                .GetOrDefault(p => p.Price);
        }

        public decimal? GetPrice(string priceTypeTC)
        {
            return Prices.AsQueryable().GetPrice(priceTypeTC);
        }

			public List<CertType> Certificates {
			get {
				var result = new List<CertType>();
				if (Course.IsTrackBool) {
					if (Course.IsSchool) {
						result.Add(CertTypes.Full.Common);
					} else {
						if (Course.IsDiplom) {
							result.Add(CertTypes.Full.Diplom);
						}
						else {
							result.Add(CertTypes.Full.Certificate);
						}
					}
				} else {
					result.Add(CertTypes.Full.Common);
					if (!Course.IsSchool) {
						result.Add(CertTypes.Full.Certificate);
					}
					if (!AuthorizationTypes.WithoutMOCert.Contains(Course.AuthorizationType_TC)) {
						result.Add(CertTypes.Full.Inter);
					}
				}
				result.Add(CertTypes.Full.Inter);
//				if (Course.IsMs) {
//					result.Add(CertTypes.Full.Ms);
//					
//				}

				result.AddRange(CertTypeList);
				return result.Distinct(x => x.UrlName).ToList();
			}
		}

	    public bool HasPPPrice {
		    get { return !CourseTC.NoSell.Contains(Course.Course_TC) 
				&& Prices.Any(x => PriceTypes.IsPP(x.PriceType_TC)); }
	    }

		public short? WebinarDiscount { get; set; }

	    public short? WebinarFinalDiscount {
		    get { return WebinarDiscount ?? MorningDiscount; }
	    }


		public Dictionary<string, short?> WebinarDiscounts { get; set; }

    }
}