using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Other
{
    public class Edge<TNode>
    {
        private int _hashCode;
        public TNode Source { get; private set; }
        public TNode Destination { get; private set; }

        public Edge( TNode source, TNode dest )
        {
            Source = source;
            Destination = dest;
            unchecked
            {
                _hashCode = source.GetHashCode() * 71 + dest.GetHashCode();
            }
        }

        public override bool Equals( object obj )
        {
            return obj is Edge<TNode> && this == (Edge<TNode>)obj;
        }

        public static bool operator ==( Edge<TNode> e1, Edge<TNode> e2 )
        {
            if( (Object)e1 == null )
            {
                return (Object)e2 == null;
            }
            else if( (Object)e2 == null )
            {
                return false;
            }
            else
            {
                return e1.Source.Equals( e2.Source ) && e1.Destination.Equals( e1.Destination );
            }
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }

    public class OutgoingEdge<TNode, TEdge>
    {
        private int _hashCode;
        public TNode Destination { get; private set; }
        public TEdge Value { get; private set; }

        public OutgoingEdge( TNode destination, TEdge value )
        {
            Destination = destination;
            Value = value;
            unchecked
            {
                _hashCode = Destination.GetHashCode() * 71 + Value.GetHashCode();
            }
        }

        public override bool Equals( object obj )
        {
            return obj is OutgoingEdge<TNode, TEdge>
                && this == (OutgoingEdge<TNode, TEdge>)obj;
        }

        public static bool operator ==( OutgoingEdge<TNode, TEdge> e1, OutgoingEdge<TNode, TEdge> e2 )
        {
            if( (Object)e1 == null )
            {
                return (Object)e2 == null;
            }
            else if( (Object)e2 == null )
            {
                return false;
            }
            else
            {
                return e1.Destination.Equals( e2.Destination )
                    && e1.Value.Equals( e1.Value );
            }
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }

    public class DirectedGraph<TNode, TEdge>
    {
        private Dictionary<TNode, LinkedListCell<OutgoingEdge<TNode, TEdge>>> _adjacencyList = new Dictionary<TNode, LinkedListCell<OutgoingEdge<TNode, TEdge>>>();

        private Dictionary<Edge<TNode>, TEdge> _edges = new Dictionary<Edge<TNode>, TEdge>();

        private int _modifications = 0;

        public void AddNode( TNode node )
        {
            if( _adjacencyList.ContainsKey( node ) )
            {
                throw new ArgumentException();
            }
            _adjacencyList.Add( node, null );

            _modifications++;
        }

        public void AddEdge( TNode source, TNode destination, TEdge value )
        {
            if( source == null || destination == null )
            {
                throw new ArgumentNullException();
            }
            if( source.Equals( destination ) )
            {
                throw new ArgumentException();
            }

            Edge<TNode> edge = new Edge<TNode>( source, destination );
            if( _edges.ContainsKey( edge ) )
            {
                throw new ArgumentException();
            }

            _edges.Add( edge, value );

            LinkedListCell<OutgoingEdge<TNode, TEdge>> list;
            _adjacencyList.TryGetValue( source, out list );
            LinkedListCell<OutgoingEdge<TNode, TEdge>> newCell = new LinkedListCell<OutgoingEdge<TNode, TEdge>>();
            newCell.Data = new OutgoingEdge<TNode, TEdge>( destination, value );
            newCell.Next = list;
            _adjacencyList[ source ] = newCell;

            _modifications++;
        }

        public IEnumerator<TNode> Nodes
        {
            get { return _adjacencyList.Keys.GetEnumerator(); }
        }

        public IEnumerator<Edge<TNode>> Edges
        {
            get { return _edges.Keys.GetEnumerator(); }
        }

        public bool ContainsNode( TNode node )
        {
            return _adjacencyList.ContainsKey( node );
        }

        public bool ContainsEdge( TNode source, TNode dest )
        {
            if( source == null || dest == null )
            {
                throw new ArgumentNullException();
            }
            if( source.Equals( dest ) )
            {
                throw new ArgumentException();
            }
            return _edges.ContainsKey( new Edge<TNode>( source, dest ) );
        }

        public TEdge GetEdgeValue( TNode source, TNode dest )
        {
            if( !ContainsEdge( source, dest ) )
            {
                throw new ArgumentException();
            }
            return _edges[ new Edge<TNode>( source, dest ) ];
        }

        public IEnumerator<OutgoingEdge<TNode, TEdge>> GetOutgoingEdges( TNode source )
        {
            LinkedListCell<OutgoingEdge<TNode, TEdge>> list;
            if( !_adjacencyList.TryGetValue( source, out list ) )
            {
                throw new ArgumentException();
            }
            int startModifications = _modifications;
            while( list != null )
            {
                if( startModifications != _modifications )
                {
                    throw new InvalidOperationException();
                }
                yield return list.Data;
            }
        }
    }
}
