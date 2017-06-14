using System;

namespace Specialist.Entities.Context {
    public partial class Vacancy {

        partial void OnCreated() {
            this.PublishDate = DateTime.Now;
        }
    }
}