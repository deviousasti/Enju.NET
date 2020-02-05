
Enju.NET
========

This is a library binding for the [Enju NLP parser][1] from Tsujii laboratory.
It features a mapping of Enju's parse tree to a fully indexed object tree, with lazy walks, both to descendants and ancestors.

> Enju is a syntactic analyzer for English. A grammar is based on
Head-driven Phrase Structure Grammar (HPSG), which is a linguistic
theory for syntax. Since this system computes more detailed structure
of sentences than CFG parsers, you can obtain various information such
as predicate-argument structures.

Usage
-----

     var enju = new EnjuClient(<port>, <host>);

The defaults are `localhost:10000`
To parse a sentence, simply call `Parse`

     Sentence sentence = enju.Parse("The sky is blue.");  

or asynchronously

     Sentence sentence = await enju.ParseAsync("The sky is blue.");  

The tree is made of a tree of constructs (type `Construct`) ending in tokens (type `Token`)
The root construct of the tree is given by:

    var root = sentence.RootConstruct;

Attributes
----------

All of the attributes given out by Enju are available on the classes as properties:

For example &lt;cons cat="S" ...
    
    var category = root.Category; //S

Walking
---------

Constructs and tokens have functions for search and tree walking. 
For example to find 'is' in "The sky is blue":

    var verb = root.Find(Category.CopulativeVerb); //is

What is?
    
    var subjectnode = verb.PrimaryRelation; //The sky

Is what?

    var prednode = verb.SecondaryRelation; // blue

Is it descriptive?

    Contract.Assert
    (
        prednode.IsAnyOf(Category.AdjectivePhrase, Category.VerbPhrase, Category.PrepositionalPhrase)
    );
   
What is the actual term used?

            Construct descterm =
            prednode.Is(Category.PrepositionalPhrase) ?
                prednode
                :
                (prednode.FindToken(PoS.Adjective) ?? prednode.FindToken(PoS.VerbGenrund)).Parent;

This also covers gerunds (like raining).

For more, see the methods of Construct, Token and Sentence;

[1]: https://github.com/mynlp/enju
