﻿using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;

namespace Specialist.Web.Cms.MetaData.Validators {
	public class CityInfoValidator:ICmsValidator<CityInfo> {
		public List<string> Validate(CityInfo entity) {
			var errors = new List<string>();
			if (!cities.Contains(entity.Name))
				errors.Add("Город не существует");
			return errors;
		}

		public static HashSet<string> cities = new HashSet<string>(
			_.List(
			"Хмельницкий",
"Каменец-Подольский",
"Нетишин",
"Славута",
"Староконстантинов",
"Шепетовка",
"Волочиск",
"Городок",
"Деражня",
"Дунаевцы",
"Изяслав",
"Красилов",
"Полонное",
"Летичев",
"Теофиполь",
"Житомир",
"Бердичев",
"Коростень",
"Новоград-Волынский",
"Малин",
"Овруч",
"Попельня",
"Винница",
"Могилёв-Подольский",
"Хмельник",
"Казатин",
"Калиновка",
"Белая Церковь",
"Бровары",
"Борисполь",
"Вишнёвое",
"Фастов",
"Буча",
"Васильков",
"Ирпень",
"Боярка",
"Обухов",
"Переяслав-Хмельницкий",
"Славутич",
"Яготин",
"Украинка",
"Вышгород",
"Богуслав",
"Коцюбинское",
"Кагарлык",
"Чернигов",
"Нежин",
"Прилуки",
"Бахмач",
"Носовка",
"Ичня",
"Варва",
"Десна",
"Сумы",
"Конотоп",
"Шостка",
"Ахтырка",
"Ромны",
"Лебедин",
"Кролевец",
"Бурынь",
"Воронеж",
"Свесса",
"Полтава",
"Кременчуг",
"Комсомольск",
"Лубны",
"Миргород",
"Гадяч",
"Карловка",
"Пирятин",
"Черкассы",
"Умань",
"Смела",
"Золотоноша",
"Канев",
"Корсунь-Шевченковский",
"Каменка",
"Чернобай",
"Кировоград",
"Александрия",
"Светловодск",
"Знаменка",
"Александровка",
"Новоархангельск",
"Киев",
"Луцк",
"Ковель",
"Нововолынск",
"Владимир-Волынский",
"Рожище",
"Шацк",
"Старая Выжевка",
"Ровно",
"Кузнецовск",
"Костополь",
"Сарны",
"Здолбунов",
"Дубровица",
"Клевань",
"Рокитное",
"Заречное",
"Львов",
"Дрогобыч",
"Червоноград",
"Стрый",
"Борислав",
"Трускавец",
"Новый Раздол",
"Золочев",
"Броды",
"Сокаль",
"Городок",
"Яворов",
"Жидачов",
"Каменка Бугская",
"Тернополь",
"Кременец",
"Бережаны",
"Збараж",
"Бучач",
"Козова",
"Ивано-Франковск",
"Калуш",
"Коломыя",
"Бурштын",
"Снятын ",
"Городенка",
"Рогатин",
"Яремче",
"Богородчаны",
"Ужгород",
"Мукачево",
"Хуст",
"Виноградов",
"Берегово",
"Свалява",
"Рахов",
"Тячев",
"Иршава",
"Королёво",
"Черновцы",
"Новоднестровск",
"Сокиряны",
"Глыбокая",
"Кельменцы",
"Кицмань",
"Харьков",
"Лозовая",
"Купянск",
"Изюм",
"Чугуев",
"Первомайский",
"Балаклея",
"Мерефа",
"Красноград",
"Песочин",
"Волчанск",
"Дергачи",
"Богодухов",
"Змиёв",
"Комсомольское",
"Солоницевка",
"Высокий",
"Кременная",
"Лутугино",
"Молодогвардейск",
"Петровское",
"Старобельск",
"Алчевск",
"Антрацит",
"Красный Луч",
"Краснодон",
"Лисичанск",
"Луганск",
"Первомайск",
"Ровеньки",
"Рубежное",
"Свердловск",
"Северодонецк",
"Стаханов",
"Новодружеск",
"Днепропетровск",
"Кривой Рог",
"Днепродзержинск",
"Никополь",
"Павлоград",
"Новомосковск",
"Жёлтые Воды",
"Марганец",
"Орджоникидзе",
"Терновка",
"Вольногорск",
"Авдеевка",
"Артёмовск",
"Горловка",
"Дебальцево",
"Дзержинск",
"Димитров",
"Доброполье",
"Докучаевск",
"Донецк",
"Дружковка",
"Енакиево",
"Константиновка",
"Краматорск",
"Красноармейск",
"Красный Лиман",
"Макеевка",
"Мариуполь",
"Новогродовка",
"Селидово",
"Славянск",
"Снежное",
"Торез",
"Угледар",
"Харцызск",
"Ясиноватая",
"Запорожье",
"Мелитополь",
"Бердянск",
"Энергодар",
"Токмак",
"Днепрорудное",
"Гуляйполе",
"Приморск",
"Николаев",
"Первомайск",
"Вознесенск",
"Южноукраинск",
"Новый Буг",
"Баштанка",
"Арбузинка",
"Херсон",
"Новая Каховка",
"Каховка",
"Цюрупинск",
"Геническ",
"Скадовск",
"Новотроицкое",
"Белозёрка",
"Одесса",
"Измаил",
"Ильичёвск",
"Белгород-Днестровский",
"Котовск",
"Южное ",
"Килия",
"Раздельная",
"Болград",
"Беляевка",
"Овидиополь",
"Теплодар",
"Вилково",
"Алупка",
"Алушта",
"Армянск",
"Бахчисарай",
"Джанкой",
"Евпатория",
"Керчь",
"Красноперекопск",
"Саки",
"Севастополь",
"Симферополь",
"Судак",
"Феодосия",
"Щёлкино",
"Ялта",
"Добромиль",
"Научный",
"Узин",
"Барышевка",
"Мамаевцы",
"Беспятное",
"Овлаши",
"Курахово",
"Оноковцы",
"Перечин",
"Великий Берёзный",
"Мостиска",
"Галич",
"Шаргород",
"Черневцы",
"Бородянка",
"Макаров",
"Чернобыль",
"Катеринополь",
"Валки",
"Близнюки",
"Кегичёвка",
"Краснокутск",
"Амвросиевка",
"Марковка",
"Нижнегорский",
"Лиманское",
"Зазимье",
"Гагарин",
"Бабаево",
"Багаевский",
"Гаврилов-ям",
"Абакан",
"Баргузин",
"Барнаул",
"Гай",
"Балабаново",
"Балаково",
"Балахна",
"Балашиха",
"Балашов",
"Байконур",
"Абинск",
"Валуйки",
"Галенки",
"Ванино",
"Батайск",
"Гатчина",
"Азов",
"Взморье",
"Братск",
"Арзгир",
"Арзамас",
"Армавир",
"Грозный",
"Бронницы",
"Арсеньев",
"Архангельск",
"Артем",
"Артемовский",
"Гремячинск",
"Грязовец",
"Брянск",
"Благовещенск",
"Благодарный",
"Глазов",
"Алапаевск",
"Владивосток",
"Владикавказ",
"Владимир",
"Биробиджан",
"Бирск",
"Бийск",
"Вилючинск",
"Аксай",
"Вихоревка",
"Альметьевск",
"Александров",
"Александровск",
"Александровск-Сахалинский",
"Александровское",
"Алексин",
"Алексеевка",
"Алдан",
"Видное",
"Ангарск",
"Бобров",
"Анапа",
"Богородск",
"Апатиты",
"Богучар",
"Богданович",
"Анадырь",
"Бор",
"Борисоглебск",
"Воркута",
"Боровичи",
"Боровск",
"Воробьевка",
"Горно-Алтайск",
"Горнозаводск",
"Воронеж",
"Городище",
"Городец",
"Анива",
"Волгоград",
"Волгодонск",
"Голицыно",
"Бологое",
"Вологда",
"Волоколамск",
"Волоконовка",
"Волосово",
"Волхов",
"Вольно-Надеждинское",
"Вольск",
"Большой Камень",
"Волжск",
"Волжский",
"Анна",
"Воскресенск",
"Воскресенское",
"Амурск",
"Воткинск",
"Внуково",
"Бодайбо",
"Анжеро-Судженск",
"Асбест",
"Астрахань",
"Всеволожск",
"Губаха",
"Губкин",
"Бузулук",
"Вурнары",
"Аткарск",
"Буйнакск",
"Гуково",
"Гулькевичи",
"Гусиноозерск",
"Гусь Хрустальный",
"Бутурлиновка",
"Гудермес",
"Буденновск",
"Выборг",
"Вырица",
"Выкса",
"Выселки",
"Вышний Волочек",
"Аэропорт Домодедово",
"Верхний Уфалей",
"Верхняя Пышма",
"Верхняя Салда",
"Березовка",
"Березовский",
"Березовский",
"Березники",
"Бердск",
"Белгород",
"Белая Калитва",
"Великий Новгород",
"Великий Устюг",
"Великие Луки",
"Белово",
"Белогорск",
"Белорецк",
"Белореченск",
"Вельск",
"Геленджик",
"Георгиевск",
"Беслан",
"Веселый",
"Вешенская",
"Адлер",
"Аша",
"Ачинск",
"Вязьма",
"Вятские Поляны",
"Забайкальск",
"Заволжье",
"Зарайск",
"Заринск",
"Заречный",
"Заполярный",
"Звенигород",
"Златоуст",
"Зима",
"Зимовники",
"Зерноград",
"Зеленоград",
"Зеленогорск",
"Зеленокумск",
"Раменское",
"Радужный",
"Радужный",
"Романовская",
"Рославль",
"Россошь",
"Ростов",
"Ростов-на-Дону",
"Родионово-Несветайская",
"Рубцовск",
"Руза",
"Рузаевка",
"Румянцево",
"Рудня",
"Рыбинск",
"Ревда",
"Ремонтное",
"Репьевка",
"Реутов",
"Рязань",
"Ряжск",
"Кагальницкая",
"Кавалерово",
"Кабанск",
"Иваново",
"Ивантеевка",
"Лабытнанги",
"Казанская",
"Казань",
"Казлук",
"Карагай",
"Карабаш",
"Калач",
"Калач-на-дону",
"Калининград",
"Калуга",
"Калтан",
"Калязин",
"Лангепас",
"Канаш",
"Канск",
"Кантемировка",
"Камышин",
"Каневская",
"Каменоломни",
"Каменск-Уральский",
"Каменск-Шахтинский",
"Камень-Рыболов",
"Кандалакша",
"Касимов",
"Кашары",
"Кашира",
"Качканар",
"Изобильный",
"Красноармейск",
"Красновишерск",
"Красногорск",
"Краснознаменск",
"Краснокамск",
"Краснокаменск",
"Красноуральск",
"Краснотурьинск",
"Красноуфимск",
"Краснодар",
"Красноярск",
"Красный Сулин",
"Красный Яр",
"Иркутск",
"Крымск",
"Иланский",
"Кизляр",
"Кизел",
"Кириши",
"Киров",
"Кировград",
"Кирово-Чепецк",
"Кировск",
"Кировск",
"Кировский",
"Киржач",
"Клин",
"Климовск",
"Иловля",
"Кимры",
"Линево",
"Липецк",
"Кинешма",
"Кисловодск",
"Киселевск",
"Когалым",
"Ковров",
"Лобня",
"Ипатово",
"Коркино",
"Королев",
"Корсаков",
"Коряжма",
"Кола",
"Коломна",
"Кольчугино",
"Конаково",
"Комсомольск-на-Амуре",
"Константиновск",
"Копейск",
"Кондопога",
"Лосино-Петровский",
"Кострома",
"Инта",
"Котово",
"Котовск",
"Котельники",
"Котельниково",
"Инжавино",
"Кодинск",
"Йошкар-Ола",
"Искитим",
"Истра",
"Кстово",
"Луга",
"Куанда",
"Кузнецк",
"Курагино",
"Курган",
"Курганинск",
"Курсавка",
"Курск",
"Курчатов",
"Кулебаки",
"Купавна",
"Кунгур",
"Кумертау",
"Луховицы",
"Кудымкар",
"Кушва",
"Лучегорск",
"Кызыл",
"Лысково",
"Лысьва",
"Лыткарино",
"Кыштым",
"Левокумское",
"Лебедянь",
"Лермонтов",
"Лениногорск",
"Ленинск",
"Ленинск-Кузнецкий",
"Ленск",
"Кемерово",
"Лесозаводск",
"Лесной",
"Лесосибирск",
"Летняя Ставка",
"Ижевск",
"Ишимбай",
"Люберцы",
"п. Лесной Городок",
"Магадан",
"Навашино",
"Павлово",
"Павловский Посад",
"Навля",
"Магнитогорск",
"Набережные Челны",
"Назрань",
"Мариинск",
"Маркс",
"Наро-Фоминск",
"Нарьян-Мар",
"Малаховка",
"Палласовка",
"Майкоп",
"Малоярославец",
"Нальчик",
"Обнинск",
"Нахабино",
"Матвеев Курган",
"Махачкала",
"Находка",
"Надым",
"Озерск",
"Орлов",
"Орловский",
"Приморско-Ахтарск",
"Прокопьевск",
"Протвино",
"Прохоровка",
"Орск",
"Орел",
"Оренбург",
"Орехово-Зуево",
"Орда",
"Миасс",
"Мирный",
"Мирный",
"Пикалево",
"Миллерово",
"Николаевск",
"Николаевск-на-Амуре",
"Микунь",
"Минусинск",
"Минеральные Воды",
"Михайловка",
"Михайловка",
"Питкяранта",
"Михнево",
"Октябрьский",
"Октябрьский",
"Ольга",
"Ольховатка",
"Оленегорск",
"Нижний Архыз",
"Нижний Новгород",
"Нижний Тагил",
"Нижние Серги",
"Нижневартовск",
"Нижнекамск",
"Нижнеудинск",
"Нижняя Салда",
"Нижняя Тура",
"Мичуринск",
"Новая Усмань",
"Ногинск",
"Новоалександровск",
"Нововоронеж",
"Новоаннинский",
"Новороссийск",
"Новокубанск",
"Новокузнецк",
"Новокуйбышевск",
"Новопавловск",
"Новониколаевский",
"Новомичуринск",
"Новомосковск",
"Новосибирск",
"Новоуральск",
"Новотроицк",
"Новодвинск",
"Новошахтинск",
"Новошахтинский",
"Могоча",
"Новочебоксарск",
"Новочеркасск",
"Новый Рогачик",
"Новый Оскол",
"Новый Ургал",
"Новый Уренгой",
"Норильск",
"Морозовск",
"Поронайск",
"Покровка",
"Полевской",
"Мончегорск",
"пос. Вешки",
"пос. Лесной",
"Омск",
"Москва",
"Можайск",
"Подольск",
"Ноябрьск",
"Оса",
"Псков",
"Муравленко",
"Отрадное",
"Мурманск",
"Муром",
"Пулково",
"Путилково",
"Пущино",
"Пушкино",
"Нытва",
"Мытищи",
"Пыть-ях",
"Мышкин",
"Мегион",
"Невинномысск",
"Невьянск",
"Первомайск",
"Первоуральск",
"Пермь",
"Переславль-Залесский",
"Нерехта",
"Нерюнгри",
"Пенза",
"Менделеево",
"Менделеевск",
"Песьянка",
"Нехаевский",
"Петрозаводск",
"Петропавловск-Камчатский",
"Нефтегорск",
"Нефтекамск",
"Нефтекумск",
"Нефтеюганск",
"Медвежьегорск",
"Медногорск",
"Междуреченск",
"Печора",
"Одинцово",
"Очер",
"Пятигорск",
"Саракташ",
"Саранск",
"Сарапул",
"Саратов",
"Саров",
"Салават",
"Салым",
"Сальск",
"Салехард",
"Самара",
"Свободный",
"Санкт-Петербург",
"Сатка",
"Сафоново",
"Светлоград",
"Светлогорск",
"Светлый",
"Светлый Яр",
"Саяногорск",
"Средняя Ахтуба",
"Сибай",
"Славянка",
"Славянск-на-Кубани",
"Сковородино",
"Собинка",
"Спасск-Дальний",
"Совхоз имени Ленина",
"Советск",
"Советская Гавань",
"Советский",
"Сортавала",
"Соликамск",
"Солнечная Долина",
"Солнечногорск",
"Смоленск",
"Сосновоборск",
"Сосновый Бор",
"Снежинск",
"Сочи",
"Ставрополь",
"Старая Русса",
"Старая Купавна",
"Старая Полтавка",
"Старая Чара",
"Старый Оскол",
"Суворов",
"Сургут",
"Стрежевой",
"Сходня",
"Сухой Лог",
"Ступино",
"Стерлитамак",
"Степное",
"Сызрань",
"Сыктывкар",
"Сысерть",
"Северобайкальск",
"Северодвинск",
"Северск",
"Сегежа",
"Сергач",
"Сергиев Посад",
"Серов",
"Серпухов",
"Сертолово",
"Серебряные Пруды",
"Селятино",
"Семикаракорск",
"Семенов",
"Сестрорецк",
"Хабаровск",
"Таганрог",
"Тарасовский",
"Тарко-сале",
"Тайга",
"Углич",
"Таксимо",
"Тайшет",
"Тамбов",
"Ханты-Мансийск",
"Хасавюрт",
"Татищево",
"Тверь",
"Увельский",
"Таштагол",
"Урай",
"Фролово",
"Троицк",
"Троицк",
"Трудовое",
"Урень",
"Трехгорный",
"Урюпинск",
"Фрязино",
"Фрязево",
"Улан-Удэ",
"Хилок",
"Тимашевск",
"Химки",
"Тихвин",
"Тихорецк",
"Ульяновск",
"Тобольск",
"Хороль",
"Фокино",
"Холмск",
"Тольятти",
"Топки",
"Томилино",
"Томск",
"Унеча",
"Усинск",
"Уссурийск",
"Усть-Илимск",
"Усть-Кинельский",
"Усть-Кут",
"Уфа",
"Туапсе",
"Тула",
"Туймазы",
"Ухта",
"Тутаев",
"Тымовское",
"Тында",
"Терней",
"Тейково",
"Темрюк",
"Удомля",
"Тюмень",
"Учалы",
"Щербинка",
"Щелково",
"Элиста",
"Электрогорск",
"Электросталь",
"Электроугли",
"Энгельс",
"Егорлык",
"Егорлыкская",
"Егорьевск",
"Ерофей-Павлович",
"Елабуга",
"Елань",
"Екатеринбург",
"Елизово",
"Ейск",
"Елец",
"Еманжелинск",
"Ессентуки",
"Ефремов",
"Далматово",
"Дальнегорск",
"Дальнереченск",
"Дзержинск",
"Дзержинский",
"Дивногорск",
"Дивное",
"Жигулевск",
"Жирновск",
"Димитровград",
"Добрянка",
"Долгопрудный",
"Дмитров",
"Домодедово",
"Донецк",
"Дубна",
"Дубовка",
"Жуковка",
"Жуковский",
"Дудинка",
"Железногорск",
"Железногорск",
"Железногорск-Илимский",
"Железноводск",
"Железнодорожный",
"Демидово",
"Десногорск",
"Дедовск",
"Цимлянск",
"Целина",
"Чайковский",
"Чалтырь",
"Чапаевск",
"Чамзинка",
"Чита",
"Чусовой",
"Чудово",
"Чебаркуль",
"Чебоксары",
"Чегдомын",
"Черкесск",
"Черниговка",
"Черногорск",
"Черноголовка",
"Чернушка",
"Чернышевск",
"Черняховск",
"Чертково",
"Череповец",
"Челябинск",
"Чехов",
"Шарыпово",
"Шарья",
"Шатура",
"Шахунья",
"Шахты",
"Шадринск",
"Шилка",
"Шимановск",
"Шумиха",
"Шумерля",
"Шебекино",
"Шерегеш",
"Шелехов",
"Юбилейный",
"Югорск",
"Юрга",
"Южно-Сахалинск",
"Южноуральск",
"Яранск",
"Ярославль",
"Ярцево",
"Яковлевка",
"Якутск",
"Янаул",
"Ясный",
"Ядрин",
"Юрюзань",
"Вязовая",
"Каспийск",
"Яхрома",
"Апрелевка",
"Покров" ) );
	}
}