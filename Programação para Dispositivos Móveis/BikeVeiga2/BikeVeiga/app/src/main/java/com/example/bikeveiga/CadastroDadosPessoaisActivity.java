package com.example.bikeveiga;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;

public class CadastroDadosPessoaisActivity extends AppCompatActivity {

    public static final String separator = ",";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cadastro_dados_pessoais);


        Button button = findViewById(R.id.btntelaconf);
        Button button1 = findViewById(R.id.btnvoltar);
        button.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                //Pegando os dados
                EditText caixanome = findViewById(R.id.caixanome);
                String nome = caixanome.getText().toString();
                EditText caixamatricula = findViewById(R.id.caixamatricula);
                String matricula = caixamatricula.getText().toString();
                EditText caixatelefone = findViewById(R.id.caixatelefone);
                String telefone = caixatelefone.getText().toString();
                EditText caixaemail = findViewById(R.id.caixaemail);
                String email = caixaemail.getText().toString();
                EditText caixanomecartao = findViewById(R.id.caixanomecartao);
                String nomecartao = caixanomecartao.getText().toString();
                EditText caixacartaonumero = findViewById(R.id.caixacartaonumero);
                String cartaonumero = caixacartaonumero.getText().toString();
                EditText caixacartaovalidade = findViewById(R.id.caixacartaovalidade);
                String cartaovalidade = caixacartaovalidade.getText().toString();
                EditText caixacartaocv = findViewById(R.id.caixacartaocv);
                String cartaocv = caixacartaocv.getText().toString();

                //RadioGroup sexo
                RadioGroup radioGroup1 = (RadioGroup) findViewById(R.id.radioGroup1);
                int radioButtonID = radioGroup1.getCheckedRadioButtonId();
                RadioButton radioButton = (RadioButton) radioGroup1.findViewById(radioButtonID);
                String sexo = (String) radioButton.getText();

                //RadioGroup Bandeira
                RadioGroup radioGroup2 = (RadioGroup) findViewById(R.id.radioGroup2);
                int radioButtonIDb = radioGroup2.getCheckedRadioButtonId();
                RadioButton radioButtonb = (RadioButton) radioGroup2.findViewById(radioButtonIDb);
                String cartbandeira = (String) radioButtonb.getText();

                //checkbox
                CheckBox checkBox1 = findViewById(R.id.checkcomputacao);
                CheckBox checkBox2 = findViewById(R.id.checkcivil);
                CheckBox checkBox3 = findViewById(R.id.checkproducao);
                CheckBox checkBox4 = findViewById(R.id.checkmecanica);


                StringBuilder stringBuilder = new StringBuilder();
                if(checkBox1.isChecked())
                stringBuilder.append(checkBox1.getText().toString());
                if(checkBox2.isChecked())
                stringBuilder.append(separator + checkBox2.getText().toString());
                if(checkBox3.isChecked())
                stringBuilder.append(separator + checkBox3.getText().toString());
                if(checkBox4.isChecked())
                stringBuilder.append(separator + checkBox4.getText().toString());


                Intent intent = new Intent(getApplicationContext(),ConfirmacaoActivity.class);
                intent.putExtra("nome",nome);
                intent.putExtra("matricula",matricula);
                intent.putExtra("telefone",telefone);
                intent.putExtra("email",email);
                intent.putExtra("nomecartao",nomecartao);
                intent.putExtra("cartaonumero",cartaonumero);
                intent.putExtra("cartaovalidade",cartaovalidade);
                intent.putExtra("cartaocv",cartaocv);
                intent.putExtra("sexo", sexo);
                intent.putExtra("cartbandeira",cartbandeira);
                intent.putExtra("curso", stringBuilder.toString());


                startActivity(intent);

            }
        });
        button1.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(),MainActivity.class);
                startActivity(intent);
            }
        });
    }




}