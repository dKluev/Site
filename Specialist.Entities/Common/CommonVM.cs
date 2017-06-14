using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Common {
    public class CommonVM<T>: IViewModel {
        public T Data { get; set; }

        public CommonVM(T data, string title) {
            Data = data;
            Title = title;
        }

        public string Title { get; set; }
    }

    public class CommonVM {
        public static CommonVM<T> New<T>(T data, string title) {
            return new CommonVM<T>(data, title);
        }
    }
}