using System.Collections.Generic;
using Specialist.Entities.Common.Const;

namespace Specialist.Entities.Order.Logic {
	public class RbkTypes {
		public const string all = "all";
			public const string bankCard = "bankCard";
			public const string terminals = "terminals";
			public const string uralsib = "uralsib";
			public const string mobilestores = "mobilestores";
			public const string postrus = "postrus";
			public const string transfers = "transfers";
			public const string ibank = "ibank";

		public static readonly Dictionary<string, string> Names =
 			new Dictionary<string, string> {
				{all, "RBK Money - Все способы оплаты"},
				{bankCard, "Банковские карты"},
				{terminals, "Платежные терминалы"},
				{uralsib, "Кассы Уралсиб"},
				{mobilestores, "Салоны связи Евросеть"},
				{postrus, "Почта России"},
				{transfers, "Системы денежных переводов"},
				{ibank, "Интернет банкинг"},
 			};
		public static readonly Dictionary<string, int> DescBlocks =
 			new Dictionary<string, int> {
				{all, HtmlBlocks.RbkTexts.All},
				{bankCard, HtmlBlocks.RbkTexts.BankCard},
				{terminals, HtmlBlocks.RbkTexts.Terminals},
				{uralsib, HtmlBlocks.RbkTexts.Uralsib},
				{mobilestores, HtmlBlocks.RbkTexts.Mobilestores},
				{postrus, HtmlBlocks.RbkTexts.Postrus},
				{transfers, HtmlBlocks.RbkTexts.Transfers},
				{ibank, HtmlBlocks.RbkTexts.Ibank},
 			};
	}
}