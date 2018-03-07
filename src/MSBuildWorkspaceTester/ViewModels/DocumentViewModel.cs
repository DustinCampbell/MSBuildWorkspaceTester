using Microsoft.CodeAnalysis;

namespace MSBuildWorkspaceTester.ViewModels
{
    internal class DocumentViewModel : HierarchyItemViewModel
    {
        private readonly DocumentId _documentId;

        public DocumentViewModel(Workspace workspace, DocumentId documentId)
            : base(workspace, isExpanded: false)
        {
            _documentId = documentId;
        }

        protected override string GetDisplayName()
            => Workspace.CurrentSolution.GetDocument(_documentId).Name;

        public override int CompareTo(HierarchyItemViewModel other)
        {
            if (other is FolderViewModel || other is ReferencesFolderViewModel)
            {
                return 1;
            }

            return base.CompareTo(other);
        }
    }
}
