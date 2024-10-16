import { Comment } from "../models/comment"
import "../style/CommentBar.css"

function CommentBar({ comment }: { comment: Comment }) {
    return (
        <div className="comment-bar">
            <p>{comment.writtenByNavigation.username}</p>
            {comment.commentText}
        </div>
  );
}

export default CommentBar;