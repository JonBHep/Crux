namespace Crux;

public class MotElement
{
    public MotElement(string spec)
    {
        int pt = spec.IndexOf(MotList.Pairconnector);
        Caption = spec.Substring(0, pt);
        string secondly = spec.Substring(pt + 1);
        pt = secondly.IndexOf(MotList.Pairconnector);
        Content = secondly.Substring(0, pt);
        string lnk = secondly.Substring(pt + 1);
        IsLink = bool.TryParse(lnk, out bool q) && q;
    }

    public MotElement(string capt, string cont, bool lnk)
    {
        Content =cont;
        Caption = capt;
        IsLink = lnk;
    }

    public string Caption { get; set; }

    public string Content { get; set; }
        
    public bool IsLink { get; set; }

    public string Specification => $"{Caption}{MotList.Pairconnector}{Content}{MotList.Pairconnector}{IsLink}";
}