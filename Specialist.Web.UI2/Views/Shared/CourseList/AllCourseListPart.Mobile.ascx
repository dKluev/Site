<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.AllCourseListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>


<%= Html.Site().MobileCourses(Model.Common.Items.Select(x => x.Course)
.Where(x => !x.IsTrackBool)) %>