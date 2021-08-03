using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

public class JsonClassCreator : EditorWindow
{
    string jsonContent = "";
    string className = "";

    [MenuItem("CustomWindow/JsonClassCreator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(JsonClassCreator));
    }

    private void OnGUI()
    {
        GUILayout.Label("");
        GUILayout.Label("Json类生产器，在下方输入框输入Json的字符串数据；");
        GUILayout.Label("输入类名，点击生成，将在Assets文件夹下生成对应的数据类");
        GUILayout.Label("");
        GUILayout.Label("Json字符串");
        jsonContent = EditorGUILayout.TextArea(jsonContent, new GUILayoutOption[] {GUILayout.MaxHeight(200)});
        className = EditorGUILayout.TextField("生成的类名", className, new GUILayoutOption[] {GUILayout.MaxWidth(250)});
        bool press = GUILayout.Button("生成类", new GUILayoutOption[] {GUILayout.Width(80), GUILayout.Height(24)});
        if (press)
        {
            Debug.Log("开始生成...");
            CreateJsonClass(jsonContent, className);
        }
        GUILayout.Label("");
        GUILayout.Label("author:gift");
        GUILayout.Label("version:1.0");
    }

    private void CreateJsonClass(string jsonContent, string className)
    {
        if (className == "")
        {
            className = "JsonClass";
        }

        string assertPath = Application.dataPath;
        int add = 0;
        string classFileName = assertPath + "/" + className + ".cs";
        while (File.Exists(classFileName))
        {
            classFileName = assertPath + "/" + className + add + ".cs";
            add++;
        }

        //名字改过
        if (add != 0)
        {
            className = className + (add - 1);
        }

        Debug.Log("准备写入文件名为" + classFileName);
        Debug.Log("开始解析Json数据...");
        StringBuilder sb = new StringBuilder();
        JObject obj = (JObject) JsonConvert.DeserializeObject(jsonContent);
        IEnumerable<JProperty> e = obj.Properties();
        WriteClass(sb, className, e);
        Debug.Log("解析Json数据完成，准备写入");
        FileStream fs = new FileStream(classFileName, FileMode.Create);
        byte[] data = System.Text.Encoding.Default.GetBytes(sb.ToString());
        Debug.Log("data.length=" + data.Length);
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
        Debug.Log("数据写入完成");
        //刷新资源
        AssetDatabase.Refresh();
        Debug.Log("数据已刷新");
    }

    private void WriteClass(StringBuilder sb, string className, IEnumerable<JProperty> e)
    {
        sb.Append("[System.Serializable]");
        sb.Append("\n");
        sb.Append("public class " + className);
        sb.Append("\n");
        sb.Append("{");
        sb.Append("\n");
        Dictionary<string, IEnumerable<JProperty>> toParseChildClassJsonString =
            new Dictionary<string, IEnumerable<JProperty>>();
        foreach (JProperty p in e)
        {
            sb.Append("public ");
            JToken value = p.Value;
            switch (value.Type)
            {
                case JTokenType.Object:
                    string firstBig = p.Name.Substring(0, 1).ToUpper();
                    string name = firstBig + p.Name.Substring(1);
                    sb.Append(name);
                    sb.Append(" ");
                    toParseChildClassJsonString.Add(name, ((JObject) (p.Value)).Properties());
                    break;
                case JTokenType.Array:
                    JArray array = (JArray) p.Value;
                    switch (array[0].Type)
                    {
                        case JTokenType.Object:
                            //需要额外生成类 最后一个s要去掉
                            string firstBig2 = p.Name.Substring(0, 1).ToUpper();
                            string name2 = firstBig2;
                            char lastChar = p.Name[p.Name.Length - 1];
                            if (lastChar == 's')
                            {
                                name2 += p.Name.Substring(1, p.Name.Length - 2);
                            }
                            else
                            {
                                name2 += p.Name.Substring(1);
                            }

                            sb.Append(name2);
                            toParseChildClassJsonString.Add(name2, ((JObject) (array[0])).Properties());
                            break;
                        case JTokenType.Integer:
                            sb.Append("int");
                            break;
                        case JTokenType.Float:
                            sb.Append("float");
                            break;
                        case JTokenType.String:
                            sb.Append("string");
                            break;
                        case JTokenType.Boolean:
                            sb.Append("bool");
                            break;
                        default:
                            sb.Append("string");
                            break;
                    }

                    sb.Append("[] ");
                    break;
                case JTokenType.Integer:
                    sb.Append("int ");
                    break;
                case JTokenType.Float:
                    sb.Append("float ");
                    break;
                case JTokenType.String:
                    sb.Append("string ");
                    break;
                case JTokenType.Boolean:
                    sb.Append("bool ");
                    break;
                default:
                    sb.Append("string ");
                    break;
            }

            sb.Append(p.Name);
            sb.Append(";\n");
        }

        foreach (var item in toParseChildClassJsonString)
        {
            WriteClass(sb, item.Key, item.Value);
        }

        sb.Append("}");
    }
}