
unsigned char num;
void main(){
     DDRB=255 ;
     TCCR1A=0b00000000;
     TCCR1B=0B00000100;
     TCCR3A=0b00000000;
     TCCR3B=0B00000010;
     
     ADMUX= 0b00100000;
     ADCSRA=0b10000111;
     ADCSRB=0b00000000;
     DIDR0= 0b00000001;
     
     UCSR0B=0b00001000;
     UCSR0C=0b00000110;
     UBRR0H=0;
     UBRR0L=103;
     
     while(1){
              if(TIFR1.B0==1)
              {
               (PORTB.b0=~PORTB.b0);
               TIFR1.B0=1;
              }
              if(TIFR3.B0==1){
                ADCSRA.B6=1;
                while(ADCSRA.B4==0){}
                ADCSRA.B4=1;

                UDR0=ADCH;
                TIFR3.B0=1;
              }

     }

}
