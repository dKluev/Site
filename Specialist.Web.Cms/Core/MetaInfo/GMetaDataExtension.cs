//using FluentMetaData.Core.Controls;
//
//namespace FluentMetaData.Core
//{
//    public static class GMetaDataExtension
//    {
//       public static GMetaData<T> AddActionLink<T>(this GMetaData<T> metaData, 
//           string displayName,
//           string action)
//       {
//           metaData.ExtraControls.Add(new ActionLinkControl(displayName,action,
//               typeof(T).Name));
//           return metaData;
//       }
//
//       public static GMetaData<T> Add<T>(this GMetaData<T> metaData,
//           CommonPropertyMetaInfo metaInfo)
//       {
//           return metaData.Add(metaInfo.PropertyName, metaInfo.DispalyName, metaInfo.Control,
//               (string)null);
//       }
//
//    }
//}