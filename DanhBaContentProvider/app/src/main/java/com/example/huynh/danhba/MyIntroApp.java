package com.example.huynh.danhba;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.github.paolorotolo.appintro.AppIntro;
import com.github.paolorotolo.appintro.AppIntroFragment;

public class MyIntroApp extends AppIntro {
    SharedPreferences sharedPreferences;
    SharedPreferences.Editor editor;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        addSlide(AppIntroFragment.newInstance("Chào mừng bạn đến với ứng dụng Danh Bạ ",
                "Với Ứng dụng này bạn có thể quản lý được các liên hệ một cách tiện dụng",R.drawable.first, ContextCompat.getColor(getApplicationContext(),R.color.firstcolor)));
        addSlide(AppIntroFragment.newInstance("Bạn muốn ...",
                "Tìm kiếm số điện thoại thật dễ dàng",R.drawable.second, ContextCompat.getColor(getApplicationContext(),R.color.secondcolor)));
        addSlide(AppIntroFragment.newInstance("Bạn Muốn ",
                "Ứng dụng có thể nghe, gọi , nhắn tin được",R.drawable.third2, ContextCompat.getColor(getApplicationContext(),R.color.thirdcolor)));
        addSlide(AppIntroFragment.newInstance("Bạn Muốn ",
                "Thông tin không bị rò rỉ ra bên ngoài",R.drawable.fourth2, ContextCompat.getColor(getApplicationContext(),R.color.colorAccent)));
        setFadeAnimation();
        sharedPreferences = getApplicationContext().getSharedPreferences("MyPreferences", Context.MODE_PRIVATE);
        editor = sharedPreferences.edit();

        if(sharedPreferences != null){
            boolean checkShared = sharedPreferences.getBoolean("checkStated",false);
            if(checkShared == true){
                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                finish();
            }
        }
    }
    @Override
    public void  onSkipPressed( Fragment currentFragment) {
        super.onSkipPressed(currentFragment);
        // Decide what to do when the user clicks on "Skip"
        startActivity(new Intent(getApplicationContext(),MainActivity.class));
        editor.putBoolean("checkStated",false).commit();
        finish();
    }

    @Override
    public void  onDonePressed(Fragment currentFragment) {
        super.onDonePressed(currentFragment);
        // Decide what to do when the user clicks on "Done"
        startActivity(new Intent(getApplicationContext(),MainActivity.class));
        editor.putBoolean("checkStated",true).commit();
        finish();
    }
}