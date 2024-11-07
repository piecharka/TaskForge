
import { useState } from "react";
import "../style/CommentBar.css"
import apiHandler from "../api/apiHandler";
import { useParams } from "react-router-dom";
import { useStore } from "../stores/store";

function CommentBar() {
    const [commentText, setCommentText] = useState("");
    const { taskId } = useParams();
    const taskIdNumber = Number(taskId);
    const { userStore } = useStore();

    const handleSubmit = (e : React.FormEvent) => {
        e.preventDefault(); 
        if (commentText.trim()) {
            apiHandler.Comments.insertComment({
                commentText,
                taskId: taskIdNumber,
                writtenBy: userStore.user.userId
            });
            setCommentText(''); 
        }
    }

    return (
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
    );
}

export default CommentBar;