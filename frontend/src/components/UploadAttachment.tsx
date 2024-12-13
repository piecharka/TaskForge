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
        <div>
            <h3>Add attachment</h3>
            <form onSubmit={handleSubmit}>
                <input
                    type="file"
                    onChange={handleFileChange}
                    accept="application/pdf,image/*"
                />
                <button type="submit" disabled={loading}>
                    {loading ? 'Sending...' : 'Add file'}
                </button>
            </form>

            {error && <div style={{ color: 'red' }}>{error}</div>}
            {success && <div style={{ color: 'green' }}>Attachment added correctly</div>}
        </div>
    );
};

export default UploadAttachment;
