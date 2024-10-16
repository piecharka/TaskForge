
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

    const handleSubmit = () => {
        apiHandler.Comments.insertComment({commentText, taskId: taskIdNumber, writtenBy : userStore.user.userId})
    }

    return (
        <div className="comment-bar">
            <form onSubmit={handleSubmit}>
                <label>Add comment</label>
                <input
                    type="comment"
                    id="comment"
                    value={commentText}
                    onChange={(e) => { setCommentText(e.target.value); } }
                />
                <button type="submit">Submit</button>
            </form>
        </div>
    );
}

export default CommentBar;