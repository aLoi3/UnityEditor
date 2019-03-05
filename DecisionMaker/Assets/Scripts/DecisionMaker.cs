using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Reflection.Emit;


public class DecisionMaker : MonoBehaviour
{
    //public System.Enum enumerator;
    //List<string> myList = new List<string>() { "Test", "Test2", "Test3" };

    //public void OnEnable()
    //{
    //    enumerator = CreateEnumFromArrays(myList);
    //}

    //public static System.Enum CreateEnumFromArrays(List<string> list)
    //{
    //    System.AppDomain currentDomain = System.AppDomain.CurrentDomain;
    //    AssemblyName aName = new AssemblyName("Enum");
    //    AssemblyBuilder ab = currentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
    //    ModuleBuilder mb = ab.DefineDynamicModule(aName.Name);
    //    EnumBuilder enumerator = mb.DefineEnum("Enum", TypeAttributes.Public, typeof(int));

    //    int i = 0;
    //    enumerator.DefineLiteral("None", i);

    //    foreach (string names in list)
    //    {
    //        i++;
    //        enumerator.DefineLiteral(names, i);
    //    }

    //    System.Type finished = enumerator.CreateType();
    //    return (System.Enum)System.Enum.ToObject(finished, 0);
    //}
}
