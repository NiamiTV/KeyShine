using UnityEngine;

namespace KSP_LogiRGB.Layout
{
    public interface ILayoutProvider
    {
        KeyCode ConvertToQwertyCode(KeyCode nativeCode);
    }
}