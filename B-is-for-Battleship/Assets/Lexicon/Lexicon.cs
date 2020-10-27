using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System;

public class Lexicon : MonoBehaviour{
    public LexiconAssetDictionary lexiconAssets;
    private readonly HashSet<string> words = new HashSet<string>();

    public void Init(Languages lang) {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        if (lexiconAssets.TryGetValue(lang, out TextAsset wordSource)) {
            using (StringReader reader = new StringReader(wordSource.text)) {
                string word = reader.ReadLine();
                while (word != null) {
                    if (word.ToUpper().All(c => char.IsLetter(c))) {
                        words.Add(word);
                    }
                    word = reader.ReadLine();
                }
            };
        } else {
            Debug.LogError($"No asset for language {lang.ToString()}");
        }
        watch.Stop();
        Debug.Log($"Lexicon initialized with {words.Count} words in {watch.Elapsed.ToString()}");
    }

    public bool Contains(string s) {
        return words.Contains(s);
    }

    /// <summary>
    /// Returns a list of indices matching words in the input string that belong to
    /// substrings matching words in this <see cref="Lexicon"/>.
    /// </summary>
    /// <param name="s">The string to look for matching substrings in</param>
    /// <returns></returns>
    public HashSet<int> FindWordIndices(string s) {
        StringBuilder b = new StringBuilder();
        HashSet<int> indices = new HashSet<int>();
        b.Append("[");
        for(int i = 0; i < s.Length; ++i) {
            for(int j = i; j < s.Length; ++j) {
                if (words.Contains(s.Substring(i, j - i + 1))) {
                    indices.UnionWith(Enumerable.Range(i, j - i + 1));
                    i = j;
                    break;
                }
                b.Append($"{s.Substring(i, j - i + 1)}, ");
            }
        }
        b.Append("]");
        Debug.Log(b.ToString());
        return new HashSet<int>();
    }
}