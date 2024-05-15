package com.example.bikeveiga;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends AppCompatActivity{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Button button = findViewById(R.id.btncadastro);
        Button button1 = findViewById(R.id.btnpedaladas);
        Button button2 = findViewById(R.id.btnagendamentos);
        Button button3 = findViewById(R.id.btnmilhas);
        button.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(),CadastroDadosPessoaisActivity.class);
                startActivity(intent);
            }
        });
        button1.setOnClickListener(new View.OnClickListener(){
           public void onClick(View v){
               Intent intent = new Intent(getApplicationContext(),PedaladasActivity.class);
               startActivity(intent);
           }
        });
        button2.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(),AgendamentosActivity.class);
                startActivity(intent);
            }
        });
        button3.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(),MilhasActivity.class);
                startActivity(intent);
            }
        });
    }
}