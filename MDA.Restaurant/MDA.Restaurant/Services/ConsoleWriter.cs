namespace MDA.Restaurant.Services;

/// <summary> Класс для вывода на консоль сообщений. </summary>
public class ConsoleWriter
{
    /// <summary> Приветствие при звонке. </summary>
    public void PhoneGreeting()
        => Console.WriteLine("Добрыый день! Подождите секунду я подберу столик и подтвержу вашу бронь, оставайтесь на линии.");

    /// <summary> Приветствие при сообщении. </summary>
    public void MessageGreeting()
        => Console.WriteLine("Добрыый день! Подождите секунду я подберу столик и подтвержу вашу бронь. Вам придет уведомление.");
}
