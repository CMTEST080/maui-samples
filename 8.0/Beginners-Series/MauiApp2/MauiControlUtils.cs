using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2
{
    public static class MauiControlUtils
    {

        public static Style GetStyleFromMergedDictionaries(string key)
        {
            foreach (var resourceDictionary in Application.Current.Resources.MergedDictionaries)
            {
                if (resourceDictionary.Keys.Contains(key))
                {
                    return resourceDictionary[key] as Style;
                }
            }

            // スタイルが見つからない場合はnullを返す
            return null;
        }
    }
}
