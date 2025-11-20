using System;
using System.Globalization;
using Omnicasa.Mobile.Zetes.iOS;

namespace Omnicasa.Mobile.Zetes;

/// <summary>CardParser</summary>
public static class CardParser
{
    /// <summary>
    /// Parse.
    /// </summary>
    /// <param name="reader">IReaderProtocol.</param>
    /// <returns>EidCardInfo.</returns>
    public static EidCardInfo Parse(this IReaderProtocol reader)
    {
        var cardInfor = new EidCardInfo();
        cardInfor.LastName = reader.LastName;

        cardInfor.FirstName = reader.FirstName;

        cardInfor.ThirdName = reader.ThirdName;

        cardInfor.CardNumber = reader.CardNumber;

        cardInfor.PlaceOfBirth = reader.PlaceOfBirth;

        cardInfor.NatNumber = reader.NatNumber;

        cardInfor.Address = reader.Address;

        cardInfor.PostalCode = reader.PostalCode;

        cardInfor.CardValidFrom = ParseDateTime(reader.CardValidFrom);

        cardInfor.CardValidTo = ParseDateTime(reader.CardValidTo);

        cardInfor.DocTypeInt = reader.DocTypeInt;

        cardInfor.SpecialStatus = reader.SpecialStatus;

        cardInfor.Sex = reader.Sex;

        cardInfor.ChipNumber = reader.ChipNumber;

        cardInfor.NobleCondition = reader.NobleCondition;

        cardInfor.Nationality = reader.Nationality;

        cardInfor.CardDeliveryMunicipality = reader.CardDeliveryMunicipality;

        try
        {
            cardInfor.Picture = reader?.Picture?.ToArray();
        }
        catch (Exception ex)
        {
            cardInfor.ExceptionMessage += ";" + ex.Message;
        }

        DateTime dateTime;
        var birthday = string.Join(
            " ",
            reader.DateOfBirth
                .Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).AsEnumerable()
                .Select(w => w.Trim()));

        string exMessageDateTime = string.Empty;
        dateTime = ParseBirthday(birthday, out exMessageDateTime);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        cardInfor.ExceptionMessage += exMessageDateTime;
        cardInfor.DateOfBirth = dateTime;

        return cardInfor;
    }

    private static int GetMonthFromText(string monthText)
    {
        try
        {
            Dictionary<int, string> monthDic = new Dictionary<int, string>();
            monthDic.Add(1, ";jan;jan;jan;");
            monthDic.Add(2, ";fev;feb;feb;");
            monthDic.Add(3, ";mars;maar;mär;");
            monthDic.Add(4, ";avr;apr;apr;");
            monthDic.Add(5, ";mai;mei;mai;");
            monthDic.Add(6, ";juin;jun;jun;");
            monthDic.Add(7, ";juil;jul;jul;");
            monthDic.Add(8, ";aout;aug;aug;");
            monthDic.Add(9, ";sept;sep;sep;");
            monthDic.Add(10, ";oct;okt;okt;");
            monthDic.Add(11, ";nov;nov;nov;");
            monthDic.Add(12, ";dec;dec;dez;");

            var value = monthDic.FirstOrDefault(kvp => kvp.Value.Contains(";" + monthText.ToLower() + ";")).Key;
            return value;
        }
        catch
        {
            // nothing
        }

        return -1;
    }

    private static DateTime ParseDateTime(string strDate)
    {
        try
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(strDate, "dd.MM.yyyy", provider);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    private static DateTime TryParseBirday(string bithday)
    {
        DateTime dateTime = DateTime.MinValue;
        try
        {
            int day = -1;
            int month = -1;
            int year = -1;
            bithday = bithday.TrimStart().TrimEnd();
            string[] paths = bithday.Split(' ');
            if (paths.Length >= 3)
            {
                foreach (string item in paths)
                {
                    var data = item.Trim();
                    int temp = -1;
                    int.TryParse(data, out temp);
                    if (temp == -1 || temp == 0)
                    {
                        month = GetMonthFromText(data);
                    }
                    else if (temp >= 1900)
                    {
                        year = temp;
                    }
                    else if (temp < 1900 && data.Length == 2)
                    {
                        day = temp;
                    }
                }

                if (day != 1 && month != 1 && year != 1)
                {
                    dateTime = new DateTime(year, month, day);
                }
            }
        }
        catch
        {
        }

        return dateTime;
    }

    private static DateTime ParseBirthday(string birthday, out string exMessage)
#pragma warning restore S1144
    {
        DateTime dateTime = DateTime.MinValue;
        exMessage = string.Empty;
        try
        {
            if (dateTime.Year <= 1900 && !string.IsNullOrEmpty(birthday))
            {
                dateTime = TryParseBirday(birthday);
                exMessage += "; birthday:" + birthday;
            }
        }
        catch (Exception ex)
        {
            exMessage += ";" + ex.Message;
        }

        return dateTime;
    }
}
