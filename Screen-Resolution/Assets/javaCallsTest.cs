using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: fix landscape mode scaling (half of buttons are out of screen), Add test automation, DAR KAZKAS BUVO

public class javaCallsTest : MonoBehaviour
{
    private readonly string TEST_STRING_GET="1111111";
    private readonly float TEST_FLOAT_GET= 1111.1f;
    private readonly int TEST_INT_GET=11111;
    private readonly bool TEST_BOOL_GET=true;
    private readonly double TEST_DOUBLE_GET=1111.11;

    private readonly string TEST_STRING_SET="222222";
    private readonly float TEST_FLOAT_SET= 2222.22f;
    private readonly int TEST_INT_SET=2222;
    private readonly bool TEST_BOOL_SET=false;
    private readonly double TEST_DOUBLE_SET=2222.22;

    private readonly string TEST_STRING_GET_obj="33333333";
    private readonly float TEST_FLOAT_GET_obj= 333333.3f;
    private readonly int TEST_INT_GET_obj=333333;
    private readonly bool TEST_BOOL_GET_obj=true;
    private readonly double TEST_DOUBLE_GET_obj=333333.33;

    private readonly string TEST_STRING_SET_obj="4444444";
    private readonly float TEST_FLOAT_SET_obj= 44444.4f;
    private readonly int TEST_INT_SET_obj=444444;
    private readonly bool TEST_BOOL_SET_obj=true;
    private readonly double TEST_DOUBLE_SET_obj=444444.44;

    private readonly int TEST_METHOD_RETURN=55555;

    private Button[] btnArray;
    // char doesnt' seem to work at all?
   // private float[] buttonSize=new float[2]{Screen.width/10, Screen.height/50};
   // private float buttonPadding=Screen.width/100;
    private Text outputField;
    private AndroidJavaObject AJO;
    #if UNITY_ANDROID
    void Start()
    {
        outputField=GetComponent<Text>();
        btnArray=transform.parent.parent.GetComponentsInChildren<Button>();
        AddListenersToButtons(btnArray);
    }

    private void AddListenersToButtons(Button[] buttons){
        buttons[0].onClick.AddListener(() => TestAndroidJavaClass_GETS());
        buttons[1].onClick.AddListener(() => TestAndroidJavaClass_SETS());
        buttons[2].onClick.AddListener(() => TestAndroidJavaClass_CALLS());
        buttons[3].onClick.AddListener(() => TestAndroidJavaClass_DISPOSE());
        
        buttons[4].onClick.AddListener(() => TestAndroidJavaObject_GETS());
        buttons[5].onClick.AddListener(() => TestAndroidJavaObject_SETS());
        buttons[6].onClick.AddListener(() => TestAndroidJavaObject_CALLS());
        buttons[7].onClick.AddListener(() => TestAndroidJavaObject_GET());
        buttons[8].onClick.AddListener(() => TestAndroidJavaObject_SET());
        buttons[9].onClick.AddListener(() => TestAndroidJavaObject_CALL());
        buttons[10].onClick.AddListener(() => TestAndroidJavaObject_DISPOSE());
    }


    public void TestAndroidJavaClass_GETS(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
         outputField.text="GET static string test: " + ((MyClass.GetStatic<string>("TEST_STRING") == TEST_STRING_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nGET static float test: " + ((MyClass.GetStatic<float>("TEST_FLOAT") == TEST_FLOAT_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nGET static int test: " + ((MyClass.GetStatic<int>("TEST_INT") == TEST_INT_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nGET static bool test: " + ((MyClass.GetStatic<bool>("TEST_BOOL") == TEST_BOOL_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nGET static double test: " + ((MyClass.GetStatic<double>("TEST_DOUBLE") == TEST_DOUBLE_GET) ? "<color=green>true</color>" : "<color=red>false</color>)");
       }
    }

    public void TestAndroidJavaClass_SETS(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
        MyClass.SetStatic<string>("TEST_STRING",TEST_STRING_SET);
        MyClass.SetStatic<float>("TEST_FLOAT", TEST_FLOAT_SET);
        MyClass.SetStatic<int>("TEST_INT", TEST_INT_SET);
        MyClass.SetStatic<bool>("TEST_BOOL", TEST_BOOL_SET);
        MyClass.SetStatic<double>("TEST_DOUBLE", TEST_DOUBLE_SET);
        
        outputField.text="SET static string test: " + ((MyClass.GetStatic<string>("TEST_STRING") == TEST_STRING_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nSET static float test: " + ((MyClass.GetStatic<float>("TEST_FLOAT") == TEST_FLOAT_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nSET static int test: " + ((MyClass.GetStatic<int>("TEST_INT") == TEST_INT_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nSET static bool test: " + ((MyClass.GetStatic<bool>("TEST_BOOL") == TEST_BOOL_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                        "\nSET static double test: " + ((MyClass.GetStatic<double>("TEST_DOUBLE") == TEST_DOUBLE_SET) ? "<color=green>true</color>" : "<color=red>false</color>)");     
        MyClass.SetStatic<string>("TEST_STRING",TEST_STRING_GET);
        MyClass.SetStatic<float>("TEST_FLOAT", TEST_FLOAT_GET);
        MyClass.SetStatic<int>("TEST_INT", TEST_INT_GET);
        MyClass.SetStatic<bool>("TEST_BOOL", TEST_BOOL_GET);
        MyClass.SetStatic<double>("TEST_DOUBLE", TEST_DOUBLE_GET);
        }
    }


    public void TestAndroidJavaClass_CALLS(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            outputField.text="CALL static method test: "+ ((MyClass.CallStatic<int>("METHODTEST_STATIC") == TEST_METHOD_RETURN ) ? "<color=green>true</color>" : "<color=red>false</color>)") +
                            "\nCALL static method with argument test: "+((MyClass.CallStatic<int>("METHODTEST_STATIC_ARGUMENTS", TEST_METHOD_RETURN) == TEST_METHOD_RETURN ) ? "<color=green>true</color>" : "<color=red>false</color>)"); 
        }
    }

    public void TestAndroidJavaClass_DISPOSE(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            MyClass.Dispose();
            try {
                Debug.Log(MyClass.GetStatic<float>("TEST_FLOAT"));
                outputField.text="class dispose test: <color=red>false</color>";
            } catch {
                outputField.text="class dispose test: <color=green>true</color>";
            }
        }
    }

// ===================================================================================================================

    
    public void TestAndroidJavaObject_GETS(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            outputField.text="GET static string test: " + ((AJO.GetStatic<string>("TEST_STRING") == TEST_STRING_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static float test: " + ((AJO.GetStatic<float>("TEST_FLOAT") == TEST_FLOAT_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static int test: " + ((AJO.GetStatic<int>("TEST_INT") == TEST_INT_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static bool test: " + ((AJO.GetStatic<bool>("TEST_BOOL") == TEST_BOOL_GET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static double test: " + ((AJO.GetStatic<double>("TEST_DOUBLE") == TEST_DOUBLE_GET) ? "<color=green>true</color>" : "<color=red>false</color>)");
        }
    }

    public void TestAndroidJavaObject_SETS(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            AJO.SetStatic<string>("TEST_STRING",TEST_STRING_SET);
            AJO.SetStatic<float>("TEST_FLOAT", TEST_FLOAT_SET);
            AJO.SetStatic<int>("TEST_INT", TEST_INT_SET);
            AJO.SetStatic<bool>("TEST_BOOL", TEST_BOOL_SET);
            AJO.SetStatic<double>("TEST_DOUBLE", TEST_DOUBLE_SET);

            outputField.text="SET static string test: " + ((AJO.GetStatic<string>("TEST_STRING") == TEST_STRING_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static float test: " + ((AJO.GetStatic<float>("TEST_FLOAT") == TEST_FLOAT_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static int test: " + ((AJO.GetStatic<int>("TEST_INT") == TEST_INT_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static bool test: " + ((AJO.GetStatic<bool>("TEST_BOOL") == TEST_BOOL_SET) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static double test: " + ((AJO.GetStatic<double>("TEST_DOUBLE") == TEST_DOUBLE_SET) ? "<color=green>true</color>" : "<color=red>false</color>)");
            AJO.SetStatic<string>("TEST_STRING",TEST_STRING_GET);
            AJO.SetStatic<float>("TEST_FLOAT", TEST_FLOAT_GET);
            AJO.SetStatic<int>("TEST_INT", TEST_INT_GET);
            AJO.SetStatic<bool>("TEST_BOOL", TEST_BOOL_GET);
            AJO.SetStatic<double>("TEST_DOUBLE", TEST_DOUBLE_GET);
        }
    }


    public void TestAndroidJavaObject_CALLS(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            outputField.text="CALL static method test: "+ ((AJO.CallStatic<int>("METHODTEST_STATIC") == TEST_METHOD_RETURN ) ? "<color=green>true</color>" : "<color=red>false</color>)") +
            "\nCALL static method with argument test: "+((AJO.CallStatic<int>("METHODTEST_STATIC_ARGUMENTS", TEST_METHOD_RETURN) == TEST_METHOD_RETURN ) ? "<color=green>true</color>" : "<color=red>false</color>)"); 
        }
    }



   public void TestAndroidJavaObject_GET(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            outputField.text="GET static string test: " + ((AJO.Get<string>("TEST_STRING_obj") == TEST_STRING_GET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static float test: " + ((AJO.Get<float>("TEST_FLOAT_obj") == TEST_FLOAT_GET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static int test: " + ((AJO.Get<int>("TEST_INT_obj") == TEST_INT_GET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static bool test: " + ((AJO.Get<bool>("TEST_BOOL_obj") == TEST_BOOL_GET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nGET static double test: " + ((AJO.Get<double>("TEST_DOUBLE_obj") == TEST_DOUBLE_GET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)");
        }
    }

    public void TestAndroidJavaObject_SET(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            AJO.Set<string>("TEST_STRING_obj",TEST_STRING_SET_obj);
            AJO.Set<float>("TEST_FLOAT_obj", TEST_FLOAT_SET_obj);
            AJO.Set<int>("TEST_INT_obj", TEST_INT_SET_obj);
            AJO.Set<bool>("TEST_BOOL_obj", TEST_BOOL_SET_obj);
            AJO.Set<double>("TEST_DOUBLE_obj", TEST_DOUBLE_SET_obj);

            outputField.text="SET static string test: " + ((AJO.Get<string>("TEST_STRING_obj") == TEST_STRING_SET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static float test: " + ((AJO.Get<float>("TEST_FLOAT_obj") == TEST_FLOAT_SET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static int test: " + ((AJO.Get<int>("TEST_INT_obj") == TEST_INT_SET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static bool test: " + ((AJO.Get<bool>("TEST_BOOL_obj") == TEST_BOOL_SET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)")+
                            "\nSET static double test: " + ((AJO.Get<double>("TEST_DOUBLE_obj") == TEST_DOUBLE_SET_obj) ? "<color=green>true</color>" : "<color=red>false</color>)");
            AJO.Set<string>("TEST_STRING_obj",TEST_STRING_GET_obj);
            AJO.Set<float>("TEST_FLOAT_obj", TEST_FLOAT_GET_obj);
            AJO.Set<int>("TEST_INT_obj", TEST_INT_GET_obj);
            AJO.Set<bool>("TEST_BOOL_obj", TEST_BOOL_GET_obj);
            AJO.Set<double>("TEST_DOUBLE_obj", TEST_DOUBLE_GET_obj);
        }
    }


    public void TestAndroidJavaObject_CALL(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            outputField.text="CALL method test: "+ ((AJO.Call<int>("METHODTEST") == TEST_METHOD_RETURN ) ? "<color=green>true</color>" : "<color=red>false</color>)" +
                            "\nCALL static method with argument test: "+((AJO.Call<int>("METHODTEST_ARGUMENTS", TEST_METHOD_RETURN) == TEST_METHOD_RETURN ) ? "<color=green>true</color>" : "<color=red>false</color>)")); 
        }
    }

    public void TestAndroidJavaObject_DISPOSE(){
        using (AndroidJavaClass MyClass = new AndroidJavaClass("com.unity.statictest.JavaPlug"))
        {
            AndroidJavaObject AJO = MyClass.CallStatic<AndroidJavaObject>("returnInstance");
            AJO.Dispose();
            try {
                Debug.Log(AJO.GetStatic<float>("TEST_FLOAT_GET_obj"));
                outputField.text="Object dispose test: <color=red>false</color>";
            } catch {
                outputField.text="Object dispose test: <color=green>true</color>";
            }
        }
    }

    #endif
}