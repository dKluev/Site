using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Passport;
using System.Linq;

namespace Specialist.Entities.Context
{
    public partial class UserMessage {
    	public const string BestGraduate = "BestGraduate";
    	public const string RealSpecialist = "Real";
    	public const string ExcelMaster = "ExcelMaster";

		public string IsAnsweredSysName {get { return IsAnswered ? "Answered" : "NotAnswered"; }}



    	public List<string> BestTypes { get; set; } 

        public bool IsGroup { get { return GroupID > 0; } }

        private EntityRef<User> _CreatorUser = default(EntityRef<User>);
        [Association(Storage = "_CreatorUser", ThisKey = "CreatorUserID",
            OtherKey = "UserID", IsForeignKey = true)]
        public User CreatorUser
        {
            get { return _CreatorUser.Entity; }
            set { _CreatorUser.Entity = value; }
        }

        private EntityRef<User> _ReceiverUser = default(EntityRef<User>);
        [Association(Storage = "_ReceiverUser", ThisKey = "ReceiverUserID",
            OtherKey = "UserID", IsForeignKey = true)]
        public User ReceiverUser
        {
            get { return _ReceiverUser.Entity; }
            set { _ReceiverUser.Entity = value; }
        }


        private EntityRef<Group> _Group = default(EntityRef<Group>);
        [Association(Storage = "_Group", ThisKey = "GroupID",
            OtherKey = "Group_ID", IsForeignKey = true)]
        public Group Group
        {
            get { return _Group.Entity; }
            set { _Group.Entity = value; }
        }

        partial void OnCreated()
        {
            PublishDate = DateTime.Today;
        }
        
    }
}