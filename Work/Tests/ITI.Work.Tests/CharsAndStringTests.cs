using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Work.Tests
{
    [TestFixture]
    class CharsAndStringTests
    {
        [Test]
        public void chars_decomposition()
        {
            string s = "Noël";
            s.Length.Should().Be( 4 );
            var sD = s.Normalize( NormalizationForm.FormD );
            sD.Length.Should().Be( 5 );
        }

        [Test]
        public void removing_diacritics()
        {
            string s = "NoëléàùçÄÎµ";
            RemoveDiacriticsNaïve( s ).Should().Be( "NoeleaucAIµ" );
        }


        [Test]
        public void testing_arabic_where_vwels_are_not_Diacritics()
        {
            string s = "الغوطة الشرق";
            var sR = RemoveDiacriticsNaïve( s );
            sR.Should().Be( s );
            sR.Should().NotBeSameAs( s );
        }

        [Test]
        public void naive_and_better_RemoveDiacritics()
        {
            string s = "kjhsdjfh 71652";
            var sRN = RemoveDiacriticsNaïve( s );
            sRN.Should().Be( s );
            sRN.Should().NotBeSameAs( s );

            var sB = RemoveDiacriticsBetter( s );
            sB.Should().BeSameAs( s );

            var sB2 = RemoveDiacriticsBetter( "â ë l" );
            sB2.Should().Be( "a e l" );
        }

        static string RemoveDiacriticsBetter( string s )
        {
            throw new NotImplementedException();
        }

        static string RemoveDiacriticsNaïve( string s )
        {
            var sD = s.Normalize( NormalizationForm.FormD );
            StringBuilder b = new StringBuilder();
            foreach( var c in sD )
            {
                if( Char.GetUnicodeCategory( c ) != UnicodeCategory.NonSpacingMark )
                {
                    b.Append( c );
                }
            }
            return b.ToString();
        }


    }
}
