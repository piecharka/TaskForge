import React, { useState } from 'react';
import apiHandler from '../api/apiHandler';

interface UploadAttachmentProps {
    taskId: number;
}

const UploadAttachment: React.FC<UploadAttachmentProps> = ({ taskId }) => {
    const [file, setFile] = useState<File | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<boolean>(false);

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files) {
            setFile(event.target.files[0]);
        }
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        if (!file) {
            setError("Choose a file");
            return;
        }

        setLoading(true);
        setError(null);
        setSuccess(false);

        try {
            const response = await apiHandler.Attachments.uploadAttachment(taskId, file);
            console.log('Attachment added', response);
            setSuccess(true);
        } catch (err) {
            setError("Error while sending attachment.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="upload-container">
            <h3 className="upload-title">Add Attachment</h3>
            <form onSubmit={handleSubmit} className="upload-form">
                <input
                    type="file"
                    onChange={handleFileChange}
                    accept="application/pdf,image/*"
                    className="file-input"
                />
                <button
                    type="submit"
                    className={`btn upload-btn ${loading ? 'loading' : ''}`}
                    disabled={loading}
                >
                    {loading ? 'Uploading...' : 'Upload File'}
                </button>
            </form>

            {error && <div className="error-message">{error}</div>}
            {success && <div className="success-message">Attachment uploaded successfully!</div>}
        </div>
    );
};

export default UploadAttachment;
