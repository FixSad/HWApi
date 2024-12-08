namespace HWBackend.Contracts;

public record CreateNode(
    string name,
    string parentId
);