This is where externally visible classes stay.

Commands and internal events for event sourcing are considered internal and should not be put here.

External events should only contain IDs and dates - their purpose is not to
synchronize internal data with external parties. Internal data should not leak
to other sources.