using System;
using Newtonsoft.Json;

namespace Omnicasa.Mobile.Zetes.Standard;

/// <inheritdoc/>
public class EidCardInfo
{
    /// <summary>
    /// Gets or sets version.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Gets or sets last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets third name.
    /// </summary>
    public string ThirdName { get; set; }

    /// <summary>
    /// Gets or sets card delivery municipality.
    /// </summary>
    public string CardDeliveryMunicipality { get; set; }

    /// <summary>
    /// Gets or sets card number.
    /// </summary>
    public string CardNumber { get; set; }

    /// <summary>
    /// Gets or sets card valid from.
    /// </summary>
    public DateTime CardValidFrom { get; set; }

    /// <summary>
    /// Gets or sets card valid to.
    /// </summary>
    public DateTime CardValidTo { get; set; }

    /// <summary>
    /// Gets or sets chip number.
    /// </summary>
    public string ChipNumber { get; set; }

    /// <summary>
    /// Gets or sets nationality.
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// Gets or sets place of birth.
    /// </summary>
    public string PlaceOfBirth { get; set; }

    /// <summary>
    /// Gets or sets sex.
    /// </summary>
    public string Sex { get; set; }

    /// <summary>
    /// Gets or sets date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets national number.
    /// </summary>
    public string NatNumber { get; set; }

    /// <summary>
    /// Gets or sets address.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets postal code.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets municipality.
    /// </summary>
    public string Municipality { get; set; }

    /// <summary>
    /// Gets or sets special status.
    /// </summary>
    public int SpecialStatus { get; set; }

    /// <summary>
    /// Gets or sets noble condition.
    /// </summary>
    public string NobleCondition { get; set; }

    /// <summary>
    /// Gets or sets special organization.
    /// </summary>
    public string SpecialOrganisation { get; set; }

    /// <summary>
    /// Gets or sets member of family.
    /// </summary>
    public string MemberOfFamily { get; set; }

    /// <summary>
    /// Gets or sets duplicate.
    /// </summary>
    public int Duplicate { get; set; }

    /// <summary>
    /// Gets or sets document type integer.
    /// </summary>
    public int DocTypeInt { get; set; }

    /// <summary>
    /// Gets or sets photo digest.
    /// </summary>
    [JsonIgnore]
    public byte[] Picture { get; set; }

    /// <summary>
    /// Exception message.
    /// </summary>
    public string ExceptionMessage { get; set; }
}
