package com.example.datc_p1.Activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

import com.example.datc_p1.R;

public class FinishActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_finish);
    }

    public void GoStatistics(View view){

        Intent myInt= new Intent(FinishActivity.this, StatisticsActivity.class);
        startActivity(myInt);
    }
}
