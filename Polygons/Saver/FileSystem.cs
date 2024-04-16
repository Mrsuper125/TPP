using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Polygons;

public partial class Saver
{
    [Obsolete]
    private void
        Save(object saveTarget,
            string? fileName = null) //This function picks object to save and saves it, reading file name from field
    {
        if (fileName == null)
        {
            fileName = _fileName;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(
            fileName,
            FileMode.Create,
            FileAccess.Write);
        bf.Serialize(fs, saveTarget);
        fs.Close();
        saved = true;
    }

    [Obsolete("Obsolete")]
    private void Load(string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        var vertices = (List<Shape>)(bf.Deserialize(fs));
        fs.Close();
        RequestDataFilling(vertices);
        saved = true;
    }
}