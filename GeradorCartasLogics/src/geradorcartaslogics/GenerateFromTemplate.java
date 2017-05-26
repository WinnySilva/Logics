/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package geradorcartaslogics;

import com.itextpdf.text.BaseColor;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.pdf.AcroFields;
import com.itextpdf.text.pdf.PdfContentByte;
import com.itextpdf.text.pdf.PdfDocument;
import com.itextpdf.text.pdf.PdfImportedPage;
import com.itextpdf.text.pdf.PdfReader;
import com.itextpdf.text.pdf.PdfStamper;
import com.itextpdf.text.pdf.PdfWriter;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Collection;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Winny S
 */
public class GenerateFromTemplate {
    
     public static void main(String[] args){
        String pathTemplate ;
            String pathSaidaCartas;
            
            PdfReader template;
            PdfStamper cartas ;
            
           
            AcroFields form;            
           String n;
           GeradorQuestao gQ = new GeradorQuestao("./sentencas1.csv");
           String cards[] ;
           pathTemplate = "./cartasAzuis.pdf";
          int level =2;
          int paginas =1;
         try {
            
            
          //  template = new PdfReader(pathTemplate);
             System.out.println("n Sentencas: "+gQ.listSize());
            
            for(int j=0;j<paginas ;j++){
                
                pathSaidaCartas = "./cartas/templateNIVEL"+level+" - "+j+".pdf";
                template = new PdfReader(pathTemplate);
                cartas = new PdfStamper(template, new FileOutputStream(pathSaidaCartas));

                

                form = cartas.getAcroFields();
                for (int i = 0; i < 9; i++) {
                    //n = (i>0)?"_"+i:"";  
                    n = "_" + i;
                    cards = gQ.GerarSentencas(level);
                    int n_card = (j==0)?(i + j*10)+1:(i + j*10);
                    form.setField("pergunta" + n, ""+n_card+"\n"+ cards[0]);
                    form.setField("resposta" + n, cards[1]);
                    
                    form.setFieldProperty("resposta", "textcolor", BaseColor.RED, null);
                    form.regenerateField("resposta");
                }
               // PdfImportedPage page = cartas.getImportedPage(template, 1);
               // cartas.insertPage(1, w_cartas.getPageSize());

                cartas.setFormFlattening(true);
                cartas.close(); template.close();
            }
            
            
        } catch (Exception ex) {
            Logger.getLogger(GenerateFromTemplate.class.getName()).log(Level.SEVERE, null, ex);
        } 
        
    }
}
