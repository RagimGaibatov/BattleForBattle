using System;

public interface ISaveable{
    int SaveId{ get; }
    SaveData Save();

    void Load(SaveData data);
}