using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriVox.Core.Messages
{
    public class FormMessages
    {
        // Success Messages
        public const string FormCreatedSuccess = "Your Form is created successfully!";
        public const string FormUpdatedSuccess = "Your Form is updated successfully!";
        public const string FormDeletedSuccess = "Your Form is deleted successfully!";
        public const string FormFound = "Your Form Found!";
        public const string FormStatusUpdatedSuccess = "Your Form status updated successfully!";

        // Failure Messages
        public const string FormNotFound = "Form not found.";
        public const string FormCreationFailed = "Form creation failed.";
        public const string FormUpdateFailed = "Form update failed.";
        public const string FormDeletionFailed = "Form deletion failed.";
    }
}
