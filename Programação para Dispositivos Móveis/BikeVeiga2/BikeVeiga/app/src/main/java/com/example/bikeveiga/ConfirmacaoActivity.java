package com.example.bikeveiga;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class ConfirmacaoActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_confirmacao);

        String separator = ",";
        Intent intent = getIntent();
        String nome = intent.getStringExtra("nome");
        String matricula = intent.getStringExtra("matricula");
        String telefone = intent.getStringExtra("telefone");
        String email = intent.getStringExtra("email");
        String nomecartao = intent.getStringExtra("nomecartao");
        String cartaonumero = intent.getStringExtra("cartaonumero");
        String cartaovalidade = intent.getStringExtra("cartaovalidade");
        String cartaocv = intent.getStringExtra("cartaocv");
        String sexo = intent.getStringExtra("sexo");
        String cartbandeira = intent.getStringExtra("cartbandeira");
        String[] curso = getIntent().getExtras().getString("curso").split(separator);
        StringBuilder stringBuilder = new StringBuilder();
        for(String s : curso) {
            stringBuilder.append(s).append(separator);
        }

        TextView confirmnome = findViewById(R.id.confirmnome);
        confirmnome.setText(nome);
        TextView confirmmatricula = findViewById(R.id.confirmmatricula);
        confirmmatricula.setText(matricula);
        TextView confirmtelefone = findViewById(R.id.confirmtelefone);
        confirmtelefone.setText(telefone);
        TextView confirmemail = findViewById(R.id.confirmemail);
        confirmemail.setText(email);
        TextView confirmcartnome = findViewById(R.id.confirmcartnome);
        confirmcartnome.setText(nomecartao);
        TextView confirmcartnumero = findViewById(R.id.confirmcartnumero);
        confirmcartnumero.setText(cartaonumero);
        TextView confirmcartvalidade = findViewById(R.id.confirmcartval);
        confirmcartvalidade.setText(cartaovalidade);
        TextView confirmcartcv = findViewById(R.id.confirmcartcv);
        confirmcartcv.setText(cartaocv);
        TextView confirmsexo = findViewById(R.id.confirmsexo);
        confirmsexo.setText(sexo);
        TextView confirmcartbandeira = findViewById(R.id.confirmcartbandeira);
        confirmcartbandeira.setText(cartbandeira);
        TextView confirmcurso = findViewById(R.id.confirmcurso);
        confirmcurso.setText(stringBuilder);

        Button button1 = findViewById(R.id.btncancelar);
        Button button2 = findViewById(R.id.btnconfirm);
        button1.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), CadastroDadosPessoaisActivity.class);
                startActivity(intent);
            }

        });
        button2.setOnClickListener(new View.OnClickListener(){
           public void onClick(View v){
               Toast.makeText(getApplicationContext(),"Dados Corretos", Toast.LENGTH_LONG).show();
           }
        });
    }
}