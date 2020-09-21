namespace TaskFromBCS
{
    /// <summary>
    /// Класс хранилище служебных сообщений в программе. 
    /// </summary>
    static class AppMessages
    {
        public static readonly string greetingTile = "Здравствуйте, данное приложение считает количество вхождений" +
                " заданных слов в группе текстовых файлов, с выводом статистики на экран или в файл.\n" +
                "В качестве шаблона для поиска используется текстовый файл в котором перечислены слова, которые будем искать\n" +
                "Вывод статистики может быть организован как с группировкой по файлам, так и общий.\n" +
                "Под словом приложение понимает набор символов, ограниченных знаками пунктуации, " +
                "пробельным символом, символами перевода строки.\nПриложение дает возможность " +
                "выбора вариантов поиска с учетом или без учета регистра.\n";

        public static readonly string requestForInputSourceFiles = "Введите путь директории с файлами для сравнения и нажмите ENTER.";
        public static readonly string requestForInputKeyFile = "Введите полное имя файла шаблона и нажмите ENTER.";
        public static readonly string requestForSelectCase = "Игнорировать регистр при подсчете статистики?\n0 - НЕТ\n1 - ДА";
        public static readonly string waitValidation = "Идет валидация файла шаблона.\nОжидайте...";
        public static readonly string successValidation = "Валидация файла шаблона успешна";
        public static readonly string failureValidation = "Валидация провалена, файл шаблона не отвечает условиям проверки.";
        public static readonly string comparingWait = "Операция учета статистики вхождения слов в файлах началась.\nОжидайте...";
        public static readonly string fileIsReadNow = "Обрабатывается файл:   ";
        public static readonly string requestForFileNameStats = "Введите полное имя файла для сохранения статистики и нажмите ENTER.";
        public static readonly string requestForOutputStatistic = "Обрбаботка файлов успешно завершена.\nУкажите, как сформировать статистику?\n0 - ГРУППИРОВКА ПО ФАЙЛАМ\n1 - ОБЩАЯ СТАТИСТИКА";
        public static readonly string requestForShowStatistic = "Укажите, как отобразить результаты операции?\n0 - СОХРАНИТЬ В ФАЙЛ\n1 - ВЫВЕСТИ В КОНСОЛЬ";
        public static readonly string endSession = "Досвидания";
    }
}
