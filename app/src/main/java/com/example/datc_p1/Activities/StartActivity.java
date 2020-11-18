package com.example.datc_p1.Activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.example.datc_p1.R;

public class StartActivity extends AppCompatActivity {

    private TextView login;
    private TextView statistics;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start);
        initializeViews();
    }

    public void SignIn(View view){

        Intent myInt= new Intent(StartActivity.this, LoginActivity.class);
        startActivity(myInt);
    }

    private void initializeViews()
    {
        login= findViewById(R.id.tv_login);
        statistics= findViewById(R.id.tv_statistics);
    }

    public void GoStatistics(View view){

        Intent myInt= new Intent(StartActivity.this, StatisticsActivity.class);
        startActivity(myInt);
    }
}
