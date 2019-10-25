package com.unity.statictest;

import android.util.Log;
import java.util.List;
import java.util.ArrayList;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

public class JavaPlug {

    private static final JavaPlug javaPlugObj = new JavaPlug();
    public static JavaPlug returnInstance(){
        return javaPlugObj;
    }

    private static String TAG="Unity";

    public static String TEST_STRING="1111111";
    public static float TEST_FLOAT= 1111.1f;
    public static int TEST_INT=11111;
    public static boolean TEST_BOOL=true;
    public static double TEST_DOUBLE=1111.11;

    public String TEST_STRING_obj="33333333";
    public float TEST_FLOAT_obj= 333333.3f;
    public int TEST_INT_obj=333333;
    public boolean TEST_BOOL_obj=true;
    public double TEST_DOUBLE_obj=333333.33;

    private static int METHODTEST_STATIC(){
        return 55555;
    }
    
    private static int METHODTEST_STATIC_ARGUMENTS(int a){
        return a;
    }

    private int METHODTEST(){
        return 55555;
    }

    private int METHODTEST_ARGUMENTS(int a){
        return a;
    }

    private void getDateTime() { 
        DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
        Date date = new Date(); 
        Log.v(TAG, "DATE AND TIME:  "+dateFormat.format(date));
    }


}
