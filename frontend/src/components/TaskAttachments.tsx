import React, { useState, useEffect } from 'react';
import apiHandler from '../api/apiHandler';  // Zaimportuj apiHandler

interface TaskAttachmentsProps {
    taskId: number;
}

const TaskAttachments: React.FC<TaskAttachmentsProps> = ({ taskId }) => {
    const [attachments, setAttachments] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    // Funkcja do pobrania plików powi¹zanych z taskId
    useEffect(() => {
        const fetchAttachments = async () => {
            setLoading(true);
            setError(null);

            try {
                const response = await apiHandler.Attachments.getAttachmentsForTask(taskId);
                setAttachments(response.data);  // Zapisz dane o za³¹cznikach
            } catch (err) {
                console.log('Error while downloading file.');
            } finally {
                setLoading(false);
            }
        };

        fetchAttachments();
    }, [taskId]);

    const handleDownload = async (attachmentId: number, filePath: string) => {
            const response = await apiHandler.Attachments.downloadAttachment(attachmentId);
            const blob = new Blob([response.data], { type: 'application/octet-stream' });

            const link = document.createElement('a');
            link.href = URL.createObjectURL(blob);
            link.download = filePath; 
            link.click();
        
    };

    return (
        <div>

            {loading && <div>Loading...</div>}
            {error && <div style={{ color: 'red' }}>{error}</div>}

            {attachments.length > 0 && (
                <div>
            <h3>Attachments</h3>
                <ul>
                    {attachments.map((attachment) => (
                        <li key={attachment.attachmentId}>
                            <button className="btn attachment-btn download-btn" onClick={() => handleDownload(attachment.attachmentId, attachment.filePath)}>
                                Download {attachment.fileName}
                            </button>
                        </li>
                    ))}
                    </ul>
                </div>
            ) }
        </div>
    );
};

export default TaskAttachments;
