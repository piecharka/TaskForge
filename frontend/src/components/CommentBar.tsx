import { Link } from "react-router-dom";
import { Comment } from "../models/comment"
import "../style/CommentBar.css"

function CommentBar({ comment }: { comment: Comment }) {
    return (
        <div className="comment-bar">
            <div className="comment-user">
                <Link to={"/users/" + comment.writtenBy}>{comment.writtenByNavigation.username}</Link>
                <span>{new Date(comment.writtenAt).toLocaleString("pl-PL", {
                    year: "numeric",
                    month: "2-digit",
                    day: "2-digit",
                    hour: "2-digit",
                    minute: "2-digit",
                    second: "2-digit"
                })}</span>
            </div>
            <p>{comment.commentText}</p>
        </div>
  );
}

export default CommentBar;