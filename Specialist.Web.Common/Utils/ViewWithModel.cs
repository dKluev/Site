namespace Specialist.Web.Common.Util
{
    public class ViewWithModel
    {
        public string View { get; private set; }

        public object Model { get; private set; }

        public ViewWithModel(string view, object model)
        {
            View = view;
            Model = model;
        }
    }
}