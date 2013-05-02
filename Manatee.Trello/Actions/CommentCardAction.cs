﻿/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		CommentCardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		CommentCardAction
	Purpose:		Indicates a member commented on a card.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a member commented on a card.
	/// </summary>
	public class CommentCardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Card _card;
		private readonly string _cardId;
		private readonly string _text;
		private readonly string _cardName;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		/// <summary>
		/// Gets the card associated with the action.
		/// </summary>
		public Card Card
		{
			get
			{
				VerifyNotExpired();
				return ((_card == null) || (_card.Id != _cardId)) && (Svc != null) ? (_card = Svc.Retrieve<Card>(_cardId)) : _card;
			}
		}
		/// <summary>
		/// The text of the comment.
		/// </summary>
		public string Text { get { return _text; } }

		/// <summary>
		/// Creates a new instance of the CommentCardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public CommentCardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board", "id");
			_cardId = action.Data.TryGetString("card","id");
			_cardName = action.Data.TryGetString("card","name");
			_text = action.Data.TryGetString("text");
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("{0} commented '{1}' on card '{2}' on {3}",
								 MemberCreator.FullName,
								 Text,
								 Card != null ? Card.Name : _cardName,
								 Date);
		}
	}
}