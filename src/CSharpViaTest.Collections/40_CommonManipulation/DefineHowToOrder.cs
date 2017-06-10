using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections._40_CommonManipulation
{
    /* 
     * Description
     * ===========
     * 
     * We have defined the sequence of a poker cards as follows. The ranks from highest to
     * lowest are:
     * 
     * Joker, 3, 2, A, K, Q, J, 10, 9, 8, 7, 6, 5, 4
     * 
     * If cards has the same ranks, the order from highest to lowest depends on their suits
     * (from highest to lowest):
     * 
     * Hearts, Diamonds, Spades, Clubs
     * 
     * Please implement the comparator to correctly order a collection of cards.
     * 
     * Difficulty: Super Easy
     * 
     * Knowledge Point
     * ===============
     * 
     * - IComparer<T>
     */
    public class DefineHowToOrder
    {
        class Card : IEquatable<Card>
        {
            public Card(CardSuit suit, CardRank rank)
            {
                if (suit == CardSuit.None && rank != CardRank.Joker)
                {
                    throw new ArgumentException("Joker has no suit.");
                }

                Suit = suit;
                Rank = rank;
            }

            public CardRank Rank { get; }
            public CardSuit Suit { get; }

            public bool Equals(Card other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Rank == other.Rank && Suit == other.Suit;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Card) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int) Rank * 397) ^ (int) Suit;
                }
            }
        }

        enum CardSuit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades,
            None
        }

        enum CardRank
        {
            RankA,
            Rank2,
            Rank3,
            Rank4,
            Rank5,
            Rank6,
            Rank7,
            Rank8,
            Rank9,
            Rank10,
            RankJ,
            RankQ,
            RankK,
            Joker
        }

        #region Please modifies the code to pass the test

        class PokerComparer : IComparer<Card>
        {
            static readonly Dictionary<CardRank, int> rankScores = new Dictionary<CardRank, int>
            {
                { CardRank.Rank4, 1 },
                { CardRank.Rank5, 2 },
                { CardRank.Rank6, 3 },
                { CardRank.Rank7, 4 },
                { CardRank.Rank8, 5 },
                { CardRank.Rank9, 6 },
                { CardRank.Rank10, 7 },
                { CardRank.RankJ, 8 },
                { CardRank.RankQ, 9 },
                { CardRank.RankK, 10 },
                { CardRank.RankA, 11 },
                { CardRank.Rank2, 12 },
                { CardRank.Rank3, 13 },
                { CardRank.Joker, 14 }
            };

            static readonly Dictionary<CardSuit, int> suitScores = new Dictionary<CardSuit, int>
            {
                { CardSuit.Clubs, 1 },
                { CardSuit.Spades, 2 },
                { CardSuit.Diamonds, 3 },
                { CardSuit.Hearts, 4 },
                { CardSuit.None, 5 }
            };

            public int Compare(Card x, Card y)
            {
                if (x == null) { throw new ArgumentNullException(nameof(x)); }
                if (y == null) { throw new ArgumentNullException(nameof(y)); }

                int rankScoreX = rankScores[x.Rank];
                int rankScoreY = rankScores[y.Rank];
                if (rankScoreX > rankScoreY) { return 1; }
                if (rankScoreX < rankScoreY) { return -1; }

                int suitScoreX = suitScores[x.Suit];
                int suitScoreY = suitScores[y.Suit];
                if (suitScoreX > suitScoreY) { return 1; }
                if (suitScoreX < suitScoreY) { return -1; }
                return 0;
            }
        }

        #endregion

        [Fact]
        public void should_order_cards_correctly()
        {
            var cards = new[]
            {
                new Card(CardSuit.Spades, CardRank.RankA),
                new Card(CardSuit.Diamonds, CardRank.Rank5),
                new Card(CardSuit.Spades, CardRank.Rank4),
                new Card(CardSuit.Hearts, CardRank.Rank5),
                new Card(CardSuit.Diamonds, CardRank.Rank2),
                new Card(CardSuit.None, CardRank.Joker),
                new Card(CardSuit.Spades, CardRank.Rank3),
                new Card(CardSuit.Clubs, CardRank.Rank7),
                new Card(CardSuit.Spades, CardRank.Rank6),
                new Card(CardSuit.Spades, CardRank.RankQ),
                new Card(CardSuit.Clubs, CardRank.Rank5)
            };
            
            var expected = new[]
            {
                new Card(CardSuit.Spades, CardRank.Rank4),
                new Card(CardSuit.Clubs, CardRank.Rank5),
                new Card(CardSuit.Diamonds, CardRank.Rank5),
                new Card(CardSuit.Hearts, CardRank.Rank5),
                new Card(CardSuit.Spades, CardRank.Rank6),
                new Card(CardSuit.Clubs, CardRank.Rank7),
                new Card(CardSuit.Spades, CardRank.RankQ),
                new Card(CardSuit.Spades, CardRank.RankA),
                new Card(CardSuit.Diamonds, CardRank.Rank2),
                new Card(CardSuit.Spades, CardRank.Rank3),
                new Card(CardSuit.None, CardRank.Joker)
            };

            Assert.Equal(expected, cards.OrderBy(c => c, new PokerComparer()));
        }
    }
}