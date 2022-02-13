using System;

namespace Crux;

public class CarteBancaire : IComparable<CarteBancaire>
{
    private const char SpecConnector = '|';
    public string Caption { get; set; }
    public string NameOnCard { get; set; }
    public string CardNumber { get; set; }
    public int FromMonth { get; set; }
    public int FromYear { get; set; }
    public int ToMonth { get; set; }
    public int ToYear { get; set; }
    public string Cvv { get; set; }
    public int Epingle { get; set; }
    public string VerificationMessage { get; set; }
    public string VerificationPassword { get; set; }
    public int CreditLimit { get; set; }
    public string Notes { get; set; }

    public CarteBancaire()
    {
        Caption = string.Empty;
        NameOnCard = string.Empty;
        CardNumber = string.Empty;
        Cvv=string.Empty;
        VerificationMessage = string.Empty;
        VerificationPassword = string.Empty;
        Notes = string.Empty;
    }
    
    private string CardNumberSpaced
    {
        get
        {
            string r = string.Empty;
            string s = CardNumber;
            while (s.Length > 4)
            {
                string p = s.Substring(0, 4);
                r += " " + p;
                s = s.Substring(4);
            }

            r += " " + s;
            return r;
        }
    }

    public string CardDetailDisplay
    {
        get
        {
            string rv = $"{Caption} {CardNumberSpaced} (exp {ToMonth:00}/{ToYear:0000})";
            return rv;
        }
    }

    public string Specification
    {
        get
        {
            string rv = Caption + SpecConnector;
            rv += NameOnCard + SpecConnector;
            rv += CardNumber + SpecConnector;
            rv += FromMonth.ToString("00") + SpecConnector;
            rv += FromYear.ToString("0000") + SpecConnector;
            rv += ToMonth.ToString("00") + SpecConnector;
            rv += ToYear.ToString("0000") + SpecConnector;
            rv += Cvv + SpecConnector;
            rv += Epingle.ToString() + SpecConnector;
            rv += VerificationMessage + SpecConnector;
            rv += VerificationPassword + SpecConnector;
            rv += CreditLimit.ToString() + SpecConnector;
            rv += Notes + SpecConnector;
            return rv;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            string[] parts = value.Split(SpecConnector);
            Caption = parts[0];
            NameOnCard = parts[1];
            CardNumber = parts[2];
            FromMonth = int.Parse(parts[3]);
            FromYear = int.Parse(parts[4]);
            ToMonth = int.Parse(parts[5]);
            ToYear = int.Parse(parts[6]);
            Cvv = parts[7];
            Epingle = int.Parse(parts[8]);
            VerificationMessage = parts[9];
            VerificationPassword = parts[10];
            CreditLimit = int.Parse(parts[11]);
            Notes = parts[12];
        }
    }

    int IComparable<CarteBancaire>.CompareTo(CarteBancaire? other)
    {
        if (other is { })
        {
            return String.Compare(this.Caption, other.Caption, StringComparison.Ordinal);    
        }

        return 0;
    }
}