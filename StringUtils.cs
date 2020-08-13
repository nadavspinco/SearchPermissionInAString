using System;
using System.Collections;
using System.Collections.Generic;


    public class StringUtils
    {
        public static List<int> searchPermissionInAString(string i_StringPattern, string i_toSearchString)
        {
            List<int> indexLists = null;
            if (i_StringPattern.Length <= i_toSearchString.Length)
            {
                indexLists = new List<int>(i_toSearchString.Length);
                int mistakes = 0;
                Dictionary<char, int> patternHash = 
                    new Dictionary<char, int>(i_StringPattern.Length);
                Dictionary<char, int> searchHash = 
                    new Dictionary<char, int>(i_StringPattern.Length);
                initDictionaries(i_StringPattern, patternHash, searchHash);
               firstSearch(i_StringPattern,i_toSearchString,
                   patternHash,searchHash,ref mistakes);
               compareDictionaries(searchHash,patternHash,ref mistakes);
               if (0 == mistakes)
                {
                    indexLists.Add((0));
                }
                compareTheResutOfTheString(i_StringPattern,i_toSearchString,
                   searchHash,patternHash,ref mistakes,
                   indexLists);
            }

            return indexLists;
        }

        private static void initDictionaries(string i_string,Dictionary<char, int> i_stringHash, Dictionary<char, int> i_searchHash)
        {
            foreach (var character in i_string)
            {
                if (i_stringHash.ContainsKey(character))
                {
                    ++i_stringHash[character];
                }
                else
                {
                    i_stringHash.Add(character, 1);
                    i_searchHash.Add(character, 0);
                }
            }

        }

        private static void firstSearch(string i_StringPattern,string i_ToSearchString 
            , Dictionary<char,int> i_StringHash, Dictionary<char,int> i_searchHash
            ,ref int io_mistakes)
        {
            for (int i = 0; i < i_StringPattern.Length; i++)
            {
                if (i_StringHash.ContainsKey(i_ToSearchString[i]))
                {
                    ++i_searchHash[i_ToSearchString[i]];
                }
                else
                {
                    ++io_mistakes;
                }
            }
        }

        private static void compareDictionaries(Dictionary<char,int> i_searchHash,
            Dictionary<char, int> i_StringHash, ref int io_mistakes)
        {
            foreach (var character in i_searchHash.Keys)
            {
                if (!i_searchHash[character].Equals(i_StringHash[character]))
                {
                    ++io_mistakes;
                }
            }
        }
        private  static void compareTheResutOfTheString(string i_string,string i_toSearchString,
            Dictionary<char,int> i_searchHash, Dictionary<char,int> i_stringHash,
            ref int io_mistakes, List<int> io_indexLists)
        {
            for (int i = i_string.Length; i < i_toSearchString.Length; i++)
            {
                if (i_searchHash.ContainsKey(i_toSearchString[i]))
                {
                    if (i_stringHash[i_toSearchString[i]].Equals(i_searchHash[i_toSearchString[i]] + 1))
                    {
                        --io_mistakes;
                    }
                    else if (i_stringHash[i_toSearchString[i]].Equals(i_searchHash[i_toSearchString[i]]))
                    {
                        io_mistakes++;
                    }
                    ++i_searchHash[i_toSearchString[i]];
                }
                else
                {
                    ++io_mistakes;
                }
                if (i_searchHash.ContainsKey(i_toSearchString[i - i_string.Length]))
                {
                    if (i_stringHash[i_toSearchString[i - i_string.Length]]
                        .Equals(i_searchHash[i_toSearchString[i - i_string.Length]]))
                    {
                        ++io_mistakes;
                    }
                    else if (i_stringHash[i_toSearchString[i - i_string.Length]]
                        .Equals(i_searchHash[i_toSearchString[i - i_string.Length]] - 1))
                    {
                        --io_mistakes;
                    }
                    --i_searchHash[i_toSearchString[i - i_string.Length]];
                }
                else
                {
                    --io_mistakes;
                }

                if (0 == io_mistakes)
                {
                    io_indexLists.Add(i - i_string.Length + 1);
                }
            }
        }
    }






