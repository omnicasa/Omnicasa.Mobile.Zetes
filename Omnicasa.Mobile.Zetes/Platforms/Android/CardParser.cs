using System.Globalization;
using BE.Zetes.Zseidlib;
using Omnicasa.Mobile.Zetes.Standard;

namespace Omnicasa.Mobile.Zetes;

/// <summary>CardParser.</summary>
public static class CardParser
{
    /// <summary>
    /// Parse.
    /// </summary>
    /// <param name="zsBleIdLib">ZsEidLib.</param>
    /// <returns>EidCardInfo.</returns>
    public static EidCardInfo Parse(this ZsEidLib zsBleIdLib)
    {
        var cardInfor = new EidCardInfo();
        var identity = zsBleIdLib.Identity;
        cardInfor.LastName = identity.Name;
        cardInfor.FirstName = identity.FirstName;
        cardInfor.ThirdName = identity.OtherName;
        cardInfor.CardNumber = identity.CardNumber;
        cardInfor.PlaceOfBirth = identity.BirthLocation;
        cardInfor.NatNumber = identity.NationalNumber;

        var address = zsBleIdLib.Address;
        cardInfor.Address = address.StreetAndNumber;
        cardInfor.PostalCode = address.Zip;
        cardInfor.CardDeliveryMunicipality = identity.CardDeliveryMunicipality;
        cardInfor.CardValidFrom = ParseDateTime(identity.CardValidityDateBegin);
        cardInfor.CardValidTo = ParseDateTime(identity.CardValidityDateEnd);
        cardInfor.DocTypeInt = ParseInt(identity.DocumentType);
        cardInfor.SpecialStatus = ParseInt(identity.SpecialStatus);
        cardInfor.Sex = identity.Sex;
        cardInfor.ChipNumber = identity.ChipNumber;
        cardInfor.NobleCondition = identity.NobleCondition;
        cardInfor.Nationality = identity.Nationality;
        cardInfor.ThirdName = identity.OtherName;
        cardInfor.Picture = zsBleIdLib.GetPicture();

        DateTime dateTime;
        var birthday = string.Join(
            " ",
            identity.BirthDate.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).AsEnumerable()
                .Select(w => w.Trim()));

        dateTime = ParseBirday(birthday);
        DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

        cardInfor.DateOfBirth = dateTime;

        return cardInfor;
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

    private static int ParseInt(string strInt)
    {
        try
        {
            return int.Parse(strInt);
        }
        catch
        {
            return -1;
        }
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

    private static DateTime ParseBirday(string birthday)
    {
        DateTime dateTime = DateTime.MinValue;
        try
        {
            if (dateTime.Year <= 1900 && !string.IsNullOrEmpty(birthday))
            {
                dateTime = TryParseBirday(birthday);
            }
        }
        catch
        {
        }

        return dateTime;
    }
}