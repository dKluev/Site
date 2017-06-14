using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection.Extensions;
using Specialist.Entities.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Web;
using Microsoft.Practices.Unity;
using Specialist.Web.Core.Utils;

namespace Specialist.Services.Core {
	public class Repository2<T> : IRepository2<T> where T : class {private static class ByIdCache<K> where K : class {
			public static Func<DataContext, object, K> GetByPK = null;
			static ByIdCache() {
				var b = typeof(ITable).IsAssignableFrom(GetExpression().Body.Type);

				GetByPK = GetExpression().Compile();
			}

		/*	public static Func<DataContext, object, K> GetByPK() {
				Expression<Func<DataContext, object, K>> expression = GetExpression();
				return CompiledQuery.Compile(expression);
			}*/

			public static Expression<Func<DataContext, object, K>> GetExpression() {
				var _type = typeof (K);
				var itemParameter = Expression.Parameter(_type, "item");
				var pkParameter = Expression.Parameter(typeof (object), "pk");
				var dcParameter = Expression.Parameter(typeof (DataContext), "dc");
				Expression<Func<IQueryable<K>, K>> exp = x => x.FirstOrDefault(y => true);
				Expression<Func<DataContext, Table<K>>> exp2 = x => x.GetTable<K>();

				var firstOrDefaultInfo = ((MethodCallExpression) exp.Body).Method;
				var getTableInfo = ((MethodCallExpression) exp2.Body).Method;


				var pkPropertyName = LinqToSqlUtils.GetPKPropertyName(_type);
				var pkProperty = Expression.Property(
					itemParameter,
					pkPropertyName
					);
				return Expression.Lambda<Func<DataContext, object, K>>(
					Expression.Call(null,
						firstOrDefaultInfo,
						Expression.Call(dcParameter, getTableInfo), Expression.Lambda<Func<K, bool>>
							(
								Expression.Equal(
									Expression.Convert(pkProperty, typeof (object)),
									pkParameter
									),
								new[] {itemParameter}
							)), new[] {dcParameter, pkParameter});
			}
		}
		private Type _type = typeof (T);
		public DataContext context { get; set; }

		public void EnableTracking() {
			context.ObjectTrackingEnabled = true;
		}

		public void LoadWith(Action<DataLoadOptionsBuilder<T>> action) {
			var builder = new DataLoadOptionsBuilder<T>(action);	
			context.LoadOptions = builder.Options;
		}
		public void LoadWith(params Expression<Func<T,object>>[] selectors) {
			LoadWith(b => b.Load(selectors));
		}
		public Repository2(IContextProvider contextProvider) {
			context = contextProvider.Get(_type);
			context.DeferredLoadingEnabled = false;
			context.ObjectTrackingEnabled = false;
#if(DEBUG)
			if (context != null)
				context.Log = new StringWriter();
#endif
		}

		public virtual T GetByPK(object pk) {
			return ByIdCache<T>.GetByPK(context, pk);
	/*		    var itemParameter = Expression.Parameter(_type, "item");

            var whereExpression = Expression.Lambda<Func<T, bool>>
                (
                    Expression.Equal(
                        Expression.Property(
                            itemParameter,
                            LinqToSqlUtils.GetPKPropertyName(_type)
                        ),
                        Expression.Constant(pk)
                    ),
                    new[] { itemParameter }
                );

            return GetAll().FirstOrDefault(whereExpression);*/
            
		}

		public static IQueryable<T> GetByPKList(IQueryable<T> list, 
			IEnumerable<object> pkList) {
			
			var idPropertyName = LinqToSqlUtils.GetPKPropertyName(typeof(T));
			var where = pkList.Select((pk, index) =>
				string.Format("{0} == @{1} ", idPropertyName, index))
				.JoinWith(" || ");
			return list.Where(where, pkList.ToArray());

		}

		public virtual IQueryable<T> GetByPK(IEnumerable<object> pkList) {

			return GetByPKList(GetAll(), pkList);

			//            return GetAll().Where(LinqToSqlUtil.WhereContains<T>(pkList.AsQueryable()));
		}


		public virtual IQueryable<T> GetAll() {
			return context.GetTable<T>();
		}


		public virtual void UpdateOrInsert(T obj) {
			var id = LinqToSqlUtils.GetPK(obj);
			if (id.Equals(LinqToSqlUtils.GetPKPropertyInfo(_type).PropertyType.Default())) {
				this.InsertAndSubmit(obj);
				return;
			}
			var oldOjb = GetByPK(id);
			oldOjb.UpdateByMeta(obj);
			context.SubmitChanges();
		}


		public void Insert(T obj) {
			context.GetTable<T>().InsertOnSubmit(obj);
		}


		public void Delete(T obj) {
			var id = LinqToSqlUtils.GetPK(obj);
			context.GetTable<T>().DeleteOnSubmit(GetByPK(id));
		}

		public K GetValues<K>(object id, Expression<Func<T,K>> selector) {
			return GetByPK(_.List(id)).Select(selector).FirstOrDefault();
		}
		public void SubmitChanges() {
		/*	var changeSet = context.GetChangeSet();
			var all = changeSet.Inserts.Concat(changeSet.Updates);
			var employeeTC = MvcApplication.Container.Resolve<IAuthService>().CurrentUser.Employee_TC
				?? Employees.Specweb;
			foreach (var entity in all) {
	        	EntityUtils.SetUpdateDateAndLastChanger(entity, employeeTC);
			}
*/			context.SubmitChanges();
		}
	}
}