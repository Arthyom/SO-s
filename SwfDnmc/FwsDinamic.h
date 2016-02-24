#ifndef FWSDINAMIC_H
#define FWSDINAMIC_H

    # include "C:/Users/frodo/Documents/Proyectos/SO's/lecturaArchivos/FwsBinStaticFile.h"

    /************************************************************************/
    /*********************** prototipos y declaracion ***********************/
    /************************************************************************/

    void       fwsDinmcFileImprmr       ( char * ruta ){
        // imprimir una cantidad de archivos solicitados
        int hdr,i, precio, stock, cadLeng, estado = 1;
        int hdr0 = FwsProdctGetHdr(ruta);
        char nombre[15];
        char delimtCampo = '*', delimtRegs = '#';
        FILE * inFile = FwsProdctCrtFl(ruta,3);
        FwsSttcsDdoble(10,"NUMERO DE ARCHIVOS A GRABAR");
        scanf("%d",&hdr);


        // imprimir el hdr en el archivo, actualizando al valor agregado
        FwsProdctActHdr(ruta,hdr,2);

        for ( i = 0 ; i < hdr ; i ++ ){
            // leer valores
            FwsSttcsDdoble(10," NOMBRE PRODUCTO");
            fflush(stdin);
            gets(nombre);


            FwsSttcsDdoble(10," PRECIO PRODUCTO ");
            fflush(stdin);
            scanf("%f",&precio);


            FwsSttcsDdoble(10,"STOCK PRODUTO ");
            fflush(stdin);
            scanf("%d",&stock);


            // imprimir estado del registro y delimitador
            fwrite(&estado, sizeof(int),1,inFile);
            fwrite( &delimtCampo, sizeof(char),1,inFile);


            // imprimir logitud de cadena y delimitador
            cadLeng = strlen(nombre);
            fwrite( &cadLeng, sizeof(int), 1, inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir nombre y delimitador
            fwrite( nombre, sizeof(char) , strlen(nombre) , inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir precio y delimitador
            fwrite( &precio, sizeof(float), 1, inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir stock y delimitador final
            fwrite( &stock, sizeof(int) , 1, inFile );
            //fwrite( &delimtCampo, sizeof(char),1,inFile);
            fwrite( &delimtRegs, sizeof(char),1,inFile);

        }

        fclose(inFile);


    }

    void       fwsDinmcFileDiplyArch    ( char * ruta ){
        FILE * outFile = FwsProdctCrtFl( ruta, 1);

        // imprimir el hdr
        printf("ENCAVEZADO [ %d ] \n", FwsProdctGetHdr(ruta) );

        char caracter;
        int  stock, contador = 0;
        float precio;

        // posicionar en el primer registro
       fseek(outFile,sizeof(int),SEEK_SET);



        // imprimir todos lo registros del archivo hasta que termine
        while ( fread(&caracter, sizeof(char), 1, outFile ) ){

            // verificar el tipo de caracter a imprimir
            switch(caracter){

                // delimitador de campo
                case '*':

                    // es el precio, leer un valor float
                    if ( contador == 0 ){

                        fread(&precio, sizeof(float), 1, outFile);
                        printf(" PRECIO [ %f ] ",precio);
                        contador++;

                    }
                    else{
                        if( contador == 1 ){
                          // leer el stock, numero entero
                          fread(&stock, sizeof(int), 1, outFile);
                          printf(" STOCK [ %d ] ",stock);
                          contador = 0;
                        }
                    }

                break;

                // delimitador de registro
                case '#':
                    printf("\n");
                break;

                    default:

                        printf("%c",caracter);

                    break;

            }
        }

        fclose(outFile);
    }

    void       fwsDinmcFileDiplyArchAlt ( char * ruta ){
        int     estado, stock, cadLeng;
        char    nombre[15], caracter, dlmtdr;
        float   precio;
        FILE    * outArch = FwsProdctCrtFl(ruta,1);

        // recorrer el archivo hasta que termine
        while ( ! feof(outArch) ){

            // leer estado del registro y delimitador
            fread(&estado, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer longitud de la cadena y delimitador
            fread(&cadLeng, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer N bits de la cadena y delimitador
            fread(nombre, sizeof(char)*cadLeng , 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el precio y el delimitador
            fread(&precio, sizeof(float), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el estock y el delimitador final
            fread(&stock, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            printf( " NOMBRE [ %s ]     PRECIO [ %f ]       ESTOKC [ %d ]       BANDERA [ %d ] \n \n", nombre, precio, stock, estado);

        }

        fclose(outArch);


    }


    ///void    fwsFileDnmcImp     ( char * ruta)



#endif // FWSDINAMIC_H
