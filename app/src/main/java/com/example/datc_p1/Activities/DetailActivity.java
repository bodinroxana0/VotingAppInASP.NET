package com.example.datc_p1.Activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.example.datc_p1.R;

public class DetailActivity extends AppCompatActivity {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detail);

    }

    public void GoVote(View view){
        Intent myInt= new Intent(DetailActivity.this, VoteActivity.class);
        startActivity(myInt);
    }

}
