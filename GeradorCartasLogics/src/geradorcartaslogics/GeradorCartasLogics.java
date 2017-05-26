/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package geradorcartaslogics;

import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Element;
import static com.itextpdf.text.ElementTags.FONT;
import com.itextpdf.text.Font;
import com.itextpdf.text.Font.FontFamily;
import com.itextpdf.text.Image;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.Phrase;
import com.itextpdf.text.Rectangle;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.ColumnText;
import com.itextpdf.text.pdf.PdfContentByte;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfTemplate;
import com.itextpdf.text.pdf.PdfWriter;
import com.itextpdf.text.pdf.codec.Base64.OutputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;

/**
 * Gerador de Cartas. iText font ROBOTO Necessaria API de texto Classe para
 * gerar as cartas do jogo Logics a partir de um arquivo csv contendo as
 * sentenças e o valor lógico da sentença. Ex: O Brasil fica na América do Sul;1
 *
 * @author Winny S
 */
public class GeradorCartasLogics {

    /**
     * http://stackoverflow.com/questions/26814958/how-to-add-text-to-an-image
     *
     * @param cb
     * @param img
     * @param watermark
     * @return
     * @throws DocumentException
     */
    public static Image getWatermarkedImage(PdfContentByte cb, Image img, String watermark) throws DocumentException, IOException {
        float width = img.getScaledWidth();
        float height = img.getScaledHeight();
        PdfTemplate template = cb.createTemplate(width, height);
        Font f = new Font(FontFamily.COURIER, 20, Font.BOLD);
        Phrase ph = new Phrase(watermark, f);
        Paragraph p = new Paragraph(watermark);
        template.addImage(img, width, 0, 0, height, 0, 0);

        //ColumnText.showTextAligned(template, Element.ALIGN_CENTER, p, width/2 , height-20 , 0);
        ColumnText ct = new ColumnText(cb);
        /*ct.setSimpleColumn(
         new Phrase("Very Long Text"),
         left=20, bottom=100, right=500, top=500,
         fontSize=18, Element.ALIGN_JUSTIFIED);
         ct.go();
         */

        //   ct.setSimpleColumn(
        //            p, 0, height, width, 0, 14, Element.ALIGN_JUSTIFIED);
        //   ct.go();
        return Image.getInstance(template);

    }

    /**
     * @param args the command line arguments
     */
   /* public static void main(String[] args) {
       GeradorQuestao gq;
        gq = new GeradorQuestao();
        
        geraPdf(3, gq);
    }
   */
    static void geraPdf(int n_cartas, GeradorQuestao gq) {
        Document doc;
        FileOutputStream os = null;
        try {
            //cria o documento tamanho A4, margens de 2,54cm
            doc = new Document(PageSize.A4, 10, 10, 10, 10);
            //cria a stream de saída
            os = new FileOutputStream("cartas.pdf");
            //associa a stream de saída ao
            PdfWriter writer = PdfWriter.getInstance(doc, os);
            //abre o documento
            doc.open();
            writer.setStrictImageSequence(true);
            Paragraph p = new Paragraph("Meu primeiro arquivo PDF!");
            //   p.setFont(f);
            Image img = Image.getInstance("rect5058.png");
            Image img2 = null;
            img.scaleToFit(170.07f, 241);
            img.setAlignment(Element.ALIGN_CENTER);
            PdfPTable table = null;//= new PdfPTable(3);
            p.add(img);
            PdfPCell cell;// = new PdfPCell(img, true);
            PdfContentByte pcb = writer.getDirectContent();
            String texto;
            float left = 20, bottom = 100, right = 190, top = 800;
            float ileft = 20, ibottom = 100, iright = 190, itop = 800;

            ColumnText ct;
            ct = new ColumnText(pcb);
            String[] auxiliarSentenca;

            for (int i = 0; i < n_cartas; i++) {
               if (i % 3 == 0) {
                    table = new PdfPTable(3);
                    table.setWidthPercentage(100.0f);
                    table.setHorizontalAlignment(Element.ALIGN_CENTER);
                }
                cell = new PdfPCell();
                auxiliarSentenca= gq.GerarSentencas(n_cartas);
                texto = auxiliarSentenca[0]+"\n\n\n"+auxiliarSentenca[1]+"\n\n\n";
                Phrase ph =new Phrase(texto);
                Font f = new Font(FontFamily.HELVETICA, 20, Font.BOLD);
               
                ph.setFont(f);

                cell.addElement(ph);
                

                cell.addElement(img2);

                table.addCell(cell);

                doc.add(table);

            }

            ct.go();

            //fechamento do documento
            doc.close();
            //fechamento da stream de saída
            os.close();

        } catch (Exception e) {
            System.out.println(" EXCEPTION ");
            e.printStackTrace();
        }
    }

}
