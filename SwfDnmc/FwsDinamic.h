#ifndef FWSDINAMIC_H
#define FWSDINAMIC_H

    # include "C:/Users/frodo/Documents/Proyectos/SO's/lecturaArchivos/FwsBinStaticFile.h"

    /************************************************************************/
    /*********************** prototipos y declaracion ***********************/
    /************************************************************************/
    void       fwsDinmcFileGet          ( char *ruta){
        // buscar el indice seleccionado
        FwsSttcsDdoble(10, "INDICE A CONSEGUIR");
        int indx;
        scanf("%d",&indx);

        int     stock, estado, indice,cadLeng,desp = 0,regst = 1;
        float   precio;
        char    nombre[15],dlmtdr,dlmtdrRgst;

        FILE * outArch = FwsProdctCrtFl(ruta,1);


        // iterar hasta que termine el archivo
        while( !feof(outArch) && indx <= FwsProdctGetHdr(ruta) ){
            // leer datos del registro

            // leer indice del registro y delimitador
            fread(&indice, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer estado del registro y delimitador
            fread(&estado, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer longitud de la cadena y delimitador
            fread(&cadLeng, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer N bits de la cadena y delimitador
            fread(nombre, sizeof(char) ,cadLeng, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el precio y el delimitador
            fread(&precio, sizeof(float), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el estock y el delimitador final
            fread(&stock, sizeof(int), 1, outArch);
            fread(&dlmtdrRgst, sizeof(char), 1, outArch);

            if ( indice == indx ){



                break;
            }
            else
                regst++;
        }

        // calcular desplazamiento
        desp = ( (5* sizeof(int) )+ (6* sizeof(char) ) + ( strlen(nombre) )  +  sizeof(float)) ;

        fseek(outArch,desp,SEEK_SET);

        // leer indice del registro y delimitador
        fread(&indice, sizeof(int), 1, outArch);
        fread(&dlmtdr, sizeof(char), 1, outArch);

        // leer estado del registro y delimitador
        fread(&estado, sizeof(int), 1, outArch);
        fread(&dlmtdr, sizeof(char), 1, outArch);

        // leer longitud de la cadena y delimitador
        fread(&cadLeng, sizeof(int), 1, outArch);
        fread(&dlmtdr, sizeof(char), 1, outArch);

        // leer N bits de la cadena y delimitador
        fread(nombre, sizeof(char) ,cadLeng, outArch);
        fread(&dlmtdr, sizeof(char), 1, outArch);

        // leer el precio y el delimitador
        fread(&precio, sizeof(float), 1, outArch);
        fread(&dlmtdr, sizeof(char), 1, outArch);

        // leer el estock y el delimitador final
        fread(&stock, sizeof(int), 1, outArch);
        fread(&dlmtdrRgst, sizeof(char), 1, outArch);

        printf( "INDICE [ %d ] NOMBRE [ %s ]     PRECIO [ %f ]       ESTOKC [ %d ]       BANDERA [ %d ] \n \n", indx, nombre, precio, stock, estado);



    }

    void       fwsDinmcFileRestaurar    ( char * ruta ){
        // buscar el indice seleccionado
        FwsSttcsDdoble(10, "INDICE A RESTAURAR");
        int indx;
        scanf("%d",&indx);

        int     stock, estado, indice,cadLeng,desp = 0,regst = 1;
        float   precio;
        char    nombre[15],dlmtdr,dlmtdrRgst;

        FILE * outArch = FwsProdctCrtFl(ruta,1);

        fseek(outArch,sizeof(int),SEEK_SET);
        // iterar hasta que termine el archivo
        while( !feof(outArch) && indx <= FwsProdctGetHdr(ruta) ){
            // leer datos del registro

            // leer indice del registro y delimitador
            fread(&indice, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer estado del registro y delimitador
            fread(&estado, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer longitud de la cadena y delimitador
            fread(&cadLeng, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer N bits de la cadena y delimitador
            fread(nombre, sizeof(char) ,cadLeng, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el precio y el delimitador
            fread(&precio, sizeof(float), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el estock y el delimitador final
            fread(&stock, sizeof(int), 1, outArch);
            fread(&dlmtdrRgst, sizeof(char), 1, outArch);

            if ( indice == indx ){
                estado = 1;
                break;
            }

            regst++;
        }

        // calcular desplazamiento
        desp = regst*( 4* sizeof(int)+ 4* sizeof(char) + strlen(nombre)+sizeof(char) + sizeof(double)+sizeof(char));

        // posiciona el cursos
        fseek(outArch,desp, SEEK_SET);

        // reimprimir los valores
        // imprimir indice del registro y delimitador
        fwrite(&indice, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir estado del registro y delimitador
        fwrite(&estado, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir longitud de la cadena y delimitador
        fwrite(&cadLeng, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir N bits de la cadena y delimitador
        fwrite(nombre, sizeof(char) ,cadLeng, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir el precio y el delimitador
        fwrite(&precio, sizeof(float), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir el estock y el delimitador final
        fwrite(&stock, sizeof(int), 1, outArch);
        fwrite(&dlmtdrRgst, sizeof(char), 1, outArch);

        fclose(outArch);

    }

    void       fwsDinmcFileBorrar       ( char * ruta ){
        // buscar el indice seleccionado
        FwsSttcsDdoble(10, "INDICE A BORRAR");
        int indx;
        scanf("%d",&indx);

        int     stock, estado, indice,cadLeng,desp = 0,regst = 1;
        float   precio;
        char    nombre[15],dlmtdr,dlmtdrRgst;

        FILE * outArch = FwsProdctCrtFl(ruta,1);

        fseek(outArch,sizeof(int),SEEK_SET);
        // iterar hasta que termine el archivo
        while( !feof(outArch) && indx <= FwsProdctGetHdr(ruta) ){
            // leer datos del registro

            // leer indice del registro y delimitador
            fread(&indice, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer estado del registro y delimitador
            fread(&estado, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer longitud de la cadena y delimitador
            fread(&cadLeng, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer N bits de la cadena y delimitador
            fread(nombre, sizeof(char) ,cadLeng, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el precio y el delimitador
            fread(&precio, sizeof(float), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el estock y el delimitador final
            fread(&stock, sizeof(int), 1, outArch);
            fread(&dlmtdrRgst, sizeof(char), 1, outArch);

            if ( indice == indx && estado == 1){
                estado = 0;
                break;
            }
            regst++;
        }


        // calcular desplazamiento
        desp =  regst*(4*sizeof(int)+ 4*sizeof(char) + strlen(nombre)+sizeof(char) + sizeof(double)+sizeof(char));

        // posiciona el cursos
        fseek(outArch,desp, SEEK_SET);

        // reimprimir los valores
        // imprimir indice del registro y delimitador
        fwrite(&indice, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir estado del registro y delimitador
        fwrite(&estado, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir longitud de la cadena y delimitador
        fwrite(&cadLeng, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir N bits de la cadena y delimitador
        fwrite(nombre, sizeof(char) ,strlen(nombre), outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir el precio y el delimitador
        fwrite(&precio, sizeof(float), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        // imprimir el estock y el delimitador final
        fwrite(&stock, sizeof(int), 1, outArch);
        fwrite(&dlmtdr, sizeof(char), 1, outArch);

        fclose(outArch);


    }

    void       fwsDinmcFileBscrIndx     ( char * ruta ){
        // buscar el indice seleccionado
        FwsSttcsDdoble(10, "INDICE A BUSCAR");
        int indx;
        scanf("%d",&indx);

        int     stock, estado, indice,cadLeng;
        float   precio;
        char    nombre[15], dlmtdr;

        FILE * outArch = FwsProdctCrtFl(ruta,1);

        fseek(outArch,sizeof(int),SEEK_SET);
        // iterar hasta que termine el archivo
        while( !feof(outArch) && indx <= FwsProdctGetHdr(ruta) ){
            // leer datos del registro

            // leer indice del registro y delimitador
            fread(&indice, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer estado del registro y delimitador
            fread(&estado, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer longitud de la cadena y delimitador
            fread(&cadLeng, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer N bits de la cadena y delimitador
            fread(nombre, sizeof(char) ,cadLeng, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el precio y el delimitador
            fread(&precio, sizeof(float), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el estock y el delimitador final
            fread(&stock, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            if ( indice == indx && estado == 1){
                printf( "INDICE [ %d ] NOMBRE [ %s ]     PRECIO [ %f ]       ESTOKC [ %d ]       BANDERA [ %d ] \n \n", indx, nombre, precio, stock, estado);

                break;
            }

        }


       fclose(outArch);


    }

    void       fwsDinmcFileImprmrAlt    ( char * ruta){
        // imprimir una cantidad de archivos solicitados
        int hdr,i, precio, stock, cadLeng, estado = 1;
        int hdr0 = FwsProdctGetHdr(ruta), indx = FwsProdctGetHdr(ruta) + 1;
        char  nombre[15]={'0'};
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
            fflush(stdin);


            FwsSttcsDdoble(10," PRECIO PRODUCTO ");
            fflush(stdin);
            scanf("%f",&precio);


            FwsSttcsDdoble(10,"STOCK PRODUTO ");
            fflush(stdin);
            scanf("%d",&stock);

            // escribir indice y delimitador
            fwrite(&indx, sizeof(int),1,inFile);
            fwrite( &delimtCampo, sizeof(char),1,inFile);

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
            fwrite( &delimtRegs, sizeof(char),1,inFile);

            indx+=1;



        }

        fclose(inFile);


    }



    void       fwsDinmcFileImprmr       ( char * ruta ){
        // imprimir una cantidad de archivos solicitados
        int hdr,i, precio, stock, cadLeng, estado = 1;
        int hdr0 = FwsProdctGetHdr(ruta);
        char  nombre[15]={'0'};
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
            fflush(stdin);


            FwsSttcsDdoble(10," PRECIO PRODUCTO ");
            fflush(stdin);
            scanf("%f",&precio);


            FwsSttcsDdoble(10,"STOCK PRODUTO ");
            fflush(stdin);
            scanf("%d",&stock);


            // imprimir nombre y delimitador
            fwrite( nombre, sizeof(char) , strlen(nombre) , inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir logitud de cadena y delimitador
            cadLeng = strlen(nombre);
            fwrite( &cadLeng, sizeof(int), 1, inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir precio y delimitador
            fwrite( &precio, sizeof(float), 1, inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir stock y delimitador final
            fwrite( &stock, sizeof(int) , 1, inFile );
            fwrite( &delimtCampo, sizeof(char),1,inFile);

            // imprimir estado del registro y delimitador
            fwrite(&estado, sizeof(int),1,inFile);
            fwrite( &delimtRegs, sizeof(char),1,inFile);



        }

        fclose(inFile);


    }

    void       fwsDinmcFileDiplyArch    ( char * ruta ){
        FILE * outFile = FwsProdctCrtFl( ruta, 1);

        // imprimir el hdr
        printf("ENCAVEZADO [ %d ] \n", FwsProdctGetHdr(ruta) );

        char caracter;
        int  stock, contador = 1 ,estado, cadLeng;
        float precio;
        int  ent;

        // posicionar en el primer registro
       fseek(outFile,sizeof(int),SEEK_SET);



        // imprimir todos lo registros del archivo hasta que termine
        while ( fread(&caracter, sizeof(char), 1, outFile ) ){

            // verificar el tipo de caracter a imprimir
            switch(caracter){

                // delimitador de campo
                case '*':
                    // arracanr segun el contador
                    switch ( contador ){

                        // estado
                        case 4:
                            fread(&estado, sizeof(int), 1, outFile);
                            printf(" %d",estado);
                            contador=1;
                            break;

                        // longitud cadena
                        case 1:
                            fread(&cadLeng, sizeof(int), 1, outFile);
                            printf(" %d",cadLeng);
                            contador++;
                        break;

                        // precio
                        case 2:
                            fread(&precio, sizeof(float), 1, outFile);
                            contador++;
                            printf(" %f",precio);
                        break;

                        // precio
                        case 3:
                            fread(&stock, sizeof(int), 1, outFile);
                            printf(" %d",stock);
                            contador++;
                        break;
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

        FILE    * outArch = FwsProdctCrtFl(ruta,1);

        int     estado, stock, cadLeng,indx;
        int     contador = 0;
        char    nombre[15]={' '}, caracter, dlmtdr;
        float   precio;




       // mostrar el encavezado
       printf("ENCAVEZADO [ %d ] \n", FwsProdctGetHdr(ruta) );

       // posicionar en el primer registro
      fseek(outArch,sizeof(int),SEEK_SET);

        // recorrer el archivo hasta que termine
        while (  !feof(outArch) ){

            // leer indice del registro y delimitador
            fread(&indx, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer estado del registro y delimitador
            fread(&estado, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer longitud de la cadena y delimitador
            fread(&cadLeng, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer N bits de la cadena y delimitador
            fread(nombre, sizeof(char) ,cadLeng, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el precio y el delimitador
            fread(&precio, sizeof(float), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            // leer el estock y el delimitador final
            fread(&stock, sizeof(int), 1, outArch);
            fread(&dlmtdr, sizeof(char), 1, outArch);

            if(dlmtdr == '#' )
            printf( "INDICE [ %d] NOMBRE [ %s ]     PRECIO [ %f ]       ESTOKC [ %d ]       BANDERA [ %d ] \n \n", indx, nombre, precio, stock, estado);

            if( feof(outArch) )
                break;

            contador++;
        }

        fclose(outArch);


    }




    ///void    fwsFileDnmcImp     ( char * ruta)



#endif // FWSDINAMIC_H
