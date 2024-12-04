import { Link, useParams } from "react-router-dom";
import { Comment } from "../models/comment"
import "../style/CommentBar.css"
import { useStore } from "../stores/store";
import { useEffect, useState } from "react";
import apiHandler from "../api/apiHandler";

function CommentBar() {
    const [comments, setComments] = useState<Comment[]>([]);
    const [commentText, setCommentText] = useState("");
    const { taskId } = useParams();
    const taskIdNumber = Number(taskId);
    const { userStore } = useStore();

    useEffect(() => {
        apiHandler.Comments.taskComments(Number(taskId))
            .then(response => setComments(response));

    }, [taskId])

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (commentText.trim()) {
            apiHandler.Comments.insertComment({
                commentText,
                taskId: taskIdNumber,
                writtenBy: userStore.user.userId
            });
            setCommentText('');
        }

        apiHandler.Comments.taskComments(Number(taskId))
            .then(response => setComments(response));
    }

    return (
        <div>

            {comments.map(comment => (
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
        ))}
            <div className="comment-bar">
                <form onSubmit={handleSubmit} className="comment-form">
                    <label htmlFor="comment">Add comment</label>
                    <textarea
                        id="comment"
                        value={commentText}
                        onChange={(e) => setCommentText(e.target.value)}
                        placeholder="Write your comment here..."
                        className="comment-input"
                    />
                    <button type="submit" className="comment-submit">Submit</button>
                </form>
            </div>
        </div>
  );
}

export default CommentBar;