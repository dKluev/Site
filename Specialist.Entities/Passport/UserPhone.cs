namespace Specialist.Entities.Passport
{
    public partial class UserContact
    {
        public UserContact(int contactTypeID, string contact)
        {
            ContactTypeID = contactTypeID;
            Contact = contact;
        }
    }
}